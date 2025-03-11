namespace Domain.Interfaces;

public interface IUserService
{
    Task<string> CreateUserAsync(string firstName, string lastName, string email, string password);
    Task<string> ChangeUserPasswordAsync(string id, string currentPassword, string newPassword);
    Task<string> ChangeUserEmailAsync(string id, string newEmail);
    Task<string> EditUserAsync(string id, string firstName, string lastName);
}