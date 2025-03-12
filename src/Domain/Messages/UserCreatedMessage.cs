namespace Domain.Messages;

public record UserCreatedMessage(Guid UserId, string FirstName, string LastName, string Email) : Message;