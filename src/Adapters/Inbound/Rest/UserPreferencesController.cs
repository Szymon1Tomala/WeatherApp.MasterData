namespace Adapters.Inbound.Rest;

public static class UserPreferencesController
{
    private const string UserPreferencesUriPrefix = "/v1/UserPreferences";

    public static void ConfigureUserPreferencesEndpoints(this WebApplication app)
    {
        app.MapGet(UserPreferencesUriPrefix, () => "UserPreferences");
    }
}