using Adapters.Inbound.Rest.Requests;
using Domain.Interfaces;
using Domain.Services;
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
                [FromServices] IUserService userService
            ) => await userService.CreateUserAsync(createUserRequest.FirstName, createUserRequest.LastName, 
                createUserRequest.Email, createUserRequest.Password));

        app.MapPatch($"{UserPreferencesUriPrefix}/{{userId}}/password", async
            (
                [FromRoute] string userId,
                [FromBody] ChangeUserPasswordRequest changeUserPasswordRequest,
                [FromServices] IUserService userService
            ) => await userService.ChangeUserPasswordAsync(userId,
                changeUserPasswordRequest.CurrentPassword, changeUserPasswordRequest.NewPassword));
        
        app.MapPatch($"{UserPreferencesUriPrefix}/{{userId}}/email", async
            (
                [FromRoute] string userId,
                [FromBody] ChangeUserEmailRequest changeUserEmailRequest,
                [FromServices] IUserService userService
            ) => await userService.ChangeUserEmailAsync(userId, changeUserEmailRequest.NewEmail));
        
        app.MapPatch($"{UserPreferencesUriPrefix}/{{userId}}", async
            (
                [FromRoute] string userId,
                [FromBody] EditUserRequest editUserRequest,
                [FromServices] IUserService userService
            ) => await userService.EditUserAsync(userId,
                editUserRequest.FirstName, editUserRequest.LastName));
    }
}