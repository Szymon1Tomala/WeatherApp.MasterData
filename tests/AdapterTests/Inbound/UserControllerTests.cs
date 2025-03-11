using Domain.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace AdapterTests.Inbound;

public partial class UserControllerTests(WebApplicationFactory<Program> factory) : BaseControllerTests(factory)
{
    private const string BaseUserControllerUri = "/v1/users";
    private const string ChangePasswordUriSuffix = "/password";
    private const string ChangeEmailUriSuffix = "/email";
    
    private readonly HttpClient _client = factory.CreateClient();
    private readonly DatabaseContext _db = factory.Services.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
}