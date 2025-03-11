namespace Adapters.Inbound.Rest.Requests;

public record ChangeUserPasswordRequest(string CurrentPassword, string NewPassword);