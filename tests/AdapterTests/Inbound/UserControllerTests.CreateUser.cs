using System.Text;
using Adapters.Inbound.Rest.Requests;
using Domain.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace AdapterTests.Inbound;

partial class UserControllerTests
{
    [Fact]
    public async Task CreateUserTest_ValidData_Success()
    {
        var createUserRequest = new CreateUserRequest("Ron", "Swanson", "ron.swanson@gmail.com", "Qwerty123!");
        
        var response = await SendRequest(HttpMethodEnum.POST, BaseUserControllerUri, createUserRequest);

        var responseMessage = await AssertOkStatusAndGetResponseMessage(response);
        
        responseMessage.Should().Be("User created successfully");

        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await db.Users.Where(x => x.Email == createUserRequest.Email).ExecuteDeleteAsync();
    }
    
    [Fact]
    public async Task CreateUserTest_DuplicatedUser_Failure()
    {
        const string email = "ron.swanson@gmail.com";
        await _db.Users.Where(x => x.Email == email).ExecuteDeleteAsync();
        
        const string firstName = "Ron";
        const string lastName = "Swanson";
        const string password = "Qwerty123!";
        
        var createUserRequest = new CreateUserRequest(firstName, lastName, email, password);
        
        var response = await SendRequest(HttpMethodEnum.POST, BaseUserControllerUri, createUserRequest);
        response.EnsureSuccessStatusCode();
        response = await SendRequest(HttpMethodEnum.POST, BaseUserControllerUri, createUserRequest);

        var responseMessage = await response.Content.ReadAsStringAsync();
        responseMessage.Should().Contain($"Can't create user with email: {email}\nUser with this email already exists");
        
        await _db.Users.Where(x => x.Email == createUserRequest.Email).ExecuteDeleteAsync();
    }
    
    private async Task<HttpResponseMessage> SendRequest<TRequest>(HttpMethodEnum method, string uri, TRequest? request = default)
    {
        return method switch
        {
            HttpMethodEnum.GET => await SendGetRequest(uri),
            HttpMethodEnum.POST => await SendPostRequest(uri, request),
            HttpMethodEnum.PUT => await SendPutRequest(uri, request),
            HttpMethodEnum.DELETE => throw new NotImplementedException(),
            HttpMethodEnum.PATCH => await SendPatchRequest(uri, request),
            _ => throw new ArgumentException($"Invalid method: {method}")
        };
    }

    private async Task<HttpResponseMessage> SendGetRequest(string uri)
    {
        return await _client.GetAsync(uri);
    }

    private async Task<HttpResponseMessage> SendPostRequest<TRequest>(string uri, TRequest? requestObject)
    {
        var request = CreateRequestStringContent(requestObject);

        return await _client.PostAsync(uri, request);
    }
    
    private async Task<HttpResponseMessage> SendPutRequest<TRequest>(string uri, TRequest? requestObject)
    {
        var request = CreateRequestStringContent(requestObject);

        return await _client.PutAsync(uri, request);
    }
    
    private async Task<HttpResponseMessage> SendPatchRequest<TRequest>(string uri, TRequest? requestObject)
    {
        var request = CreateRequestStringContent(requestObject);

        return await _client.PatchAsync(uri, request);
    }

    private static StringContent CreateRequestStringContent<TRequest>(TRequest? requestObject)
    {
        var serialized = JsonConvert.SerializeObject(requestObject);
        return new StringContent(serialized, Encoding.UTF8, "application/json");
    }

    private static async Task<string> AssertOkStatusAndGetResponseMessage(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}