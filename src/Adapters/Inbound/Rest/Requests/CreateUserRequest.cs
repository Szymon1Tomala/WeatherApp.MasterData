namespace Adapters.Inbound.Rest.Requests;

public record CreateUserRequest(string FirstName, string LastName, string Email, string Password);