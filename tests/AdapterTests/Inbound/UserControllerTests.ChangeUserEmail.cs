using Adapters.Inbound.Rest.Requests;
using Domain.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdapterTests.Inbound;

partial class UserControllerTests
{
    /* 
    [Fact]
    public async Task ChangeUserEmail_ValidData_Success()
    {
        using var scope = factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        
        const string firstName = "Ron";
        const string lastName = "Swanson";
        const string email = "ron.swanson@gmail.com";
        const string password = "Qwerty123!";
        
        var createdUser = await CreateAndGetUser(firstName, lastName, email, password, db);
        var changeUserEmailRequest = new ChangeUserEmailRequest(email);

        var response = await SendRequest(HttpMethodEnum.PATCH,
            $"{BaseUserControllerUri}/{createdUser.Id}{ChangeEmailUriSuffix}", changeUserEmailRequest);

        var responseMessage = await response.Content.ReadAsStringAsync();
        responseMessage.Should().Be("Email changed successfully");
        
        await db.Users.Where(x => x.Id == createdUser.Id).ExecuteDeleteAsync();
    }
    */
}