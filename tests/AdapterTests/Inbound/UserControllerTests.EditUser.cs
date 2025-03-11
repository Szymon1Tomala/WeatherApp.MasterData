using Adapters.Inbound.Rest.Requests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AdapterTests.Inbound;

partial class UserControllerTests
{
    [Fact]
    public async Task EditUserTest_DuplicatedUser_Failure()
    {
        const string email = "ron.swanson@gmail.com";
        await _db.Users.Where(x => x.Email == email).ExecuteDeleteAsync();
        
        const string firstName = "Ron";
        const string lastName = "Swanson";
        const string password = "Qwerty123!";
        
        var createUserRequest = new CreateUserRequest(firstName, lastName, email, password);
        
        var response = await SendRequest(HttpMethodEnum.POST, BaseUserControllerUri, createUserRequest);
        response.EnsureSuccessStatusCode();
        var createdUser = await _db.Users.FirstAsync(x => x.Email == email);
        
        var editUserRequest = new EditUserRequest(lastName, firstName);
        response = await SendRequest(HttpMethodEnum.PATCH, $"{BaseUserControllerUri}/{createdUser.Id}", editUserRequest);

        var responseMessage = await response.Content.ReadAsStringAsync();
        responseMessage.Should().Contain($"User edited successfully");
        
        await _db.Users.Where(x => x.Id == createdUser.Id).ExecuteDeleteAsync();
    }
}