namespace Domain.Interfaces.Users;

public interface IChangeUserPasswordService
{
    Task<string> ChangeUserPasswordAsync(string id, string currentPassword, string newPassword);
}