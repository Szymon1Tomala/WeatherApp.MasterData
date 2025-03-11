namespace Domain.Interfaces.Users;

public interface IChangeUserEmailService
{
    Task<string> ChangeUserEmailAsync(string id, string newEmail);
}