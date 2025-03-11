using Adapters.Inbound.Rest.Requests;
using Domain.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdapterTests.Inbound;

partial class UserControllerTests
{
    [Fact]
    public async Task ChangeUserPassword_ValidData_Success()
    {
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        
        const string firstName = "Ron";
        const string lastName = "Swanson";
        const string email = "ron.swanson@gmail.com";
        const string password = "Qwerty123!";
        const string newPassword = "Asdfg4567%";
        
        var createUserRequest = new CreateUserRequest(firstName, lastName, email, password);
        
        var createUserResponse = await SendRequest(HttpMethodEnum.POST, BaseUserControllerUri, createUserRequest);
        await AssertOkStatusAndGetResponseMessage(createUserResponse);
        var createdUser = await db.Users.FirstAsync(x => x.Email == email);
        
        var changeUserPasswordRequest = new ChangeUserPasswordRequest(password, newPassword);

        var response = await SendRequest(HttpMethodEnum.PATCH,
            $"{BaseUserControllerUri}/{createdUser.Id}{ChangePasswordUriSuffix}", changeUserPasswordRequest);

        var responseMessage = await response.Content.ReadAsStringAsync();
        responseMessage.Should().Be("Password changed successfully");
        
        await db.Users.Where(x => x.Id == createdUser.Id).ExecuteDeleteAsync();
    }
}