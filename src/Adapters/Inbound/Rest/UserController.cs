using Adapters.Inbound.Rest.Requests;
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
                CancellationToken cancellationToken
            ) =>
        {
            return "";
        });
    }
}