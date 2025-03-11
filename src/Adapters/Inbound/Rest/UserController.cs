using Adapters.Inbound.Rest.Requests;
using Domain.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.Rest;

public static class UserController
{
    private const string UserPreferencesUriPrefix = "/v1/users";

    public static void ConfigureUserPreferencesEndpoints(this WebApplication app)
    {
        app.MapPost(UserPreferencesUriPrefix, async 
            (
                [FromBody] CreateUserRequest createUserRequest,
                [FromServices] IUserCreationService userService
            ) => await userService.CreateUserAsync(createUserRequest.FirstName, createUserRequest.LastName, 
                createUserRequest.Email, createUserRequest.Password));

        app.MapPatch($"{UserPreferencesUriPrefix}/{{userId}}/password", async
            (
                [FromRoute] string userId,
                [FromBody] ChangeUserPasswordRequest changeUserPasswordRequest,
                [FromServices] IChangeUserPasswordService userService
            ) => await userService.ChangeUserPasswordAsync(userId,
                changeUserPasswordRequest.CurrentPassword, changeUserPasswordRequest.NewPassword));
        
        app.MapPatch($"{UserPreferencesUriPrefix}/{{userId}}/email", async
            (
                [FromRoute] string userId,
                [FromBody] ChangeUserEmailRequest changeUserEmailRequest,
                [FromServices] IChangeUserEmailService userService
            ) => await userService.ChangeUserEmailAsync(userId, changeUserEmailRequest.NewEmail));
        
        app.MapPatch($"{UserPreferencesUriPrefix}/{{userId}}", async
            (
                [FromRoute] string userId,
                [FromBody] EditUserRequest editUserRequest,
                [FromServices] IEditUserService userService
            ) => await userService.EditUserAsync(userId,
                editUserRequest.FirstName, editUserRequest.LastName));
    }
}