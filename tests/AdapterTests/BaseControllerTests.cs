using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AdapterTests;

public class BaseControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>;