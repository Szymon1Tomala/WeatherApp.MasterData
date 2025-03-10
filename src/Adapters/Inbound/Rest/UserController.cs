using Adapters.Inbound.Rest.Requests;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.Rest;

public static class UserController
{
    private const string UserPreferencesUriPrefix = "/v1/users";

    public static void ConfigureUserPreferencesEndpoints(this WebApplication app)
    {
        app.MapGet($"{UserPreferencesUriPrefix}/create", () => "UserPreferences");
        
        app.MapPost(UserPreferencesUriPrefix, async 
            (
                [FromBody] CreateUserRequest createUserRequest,
                [FromServices] UserService userService
            ) => await userService.CreateUserAsync(createUserRequest.FirstName, createUserRequest.LastName, 
            createUserRequest.Email, createUserRequest.Password));
    }
}