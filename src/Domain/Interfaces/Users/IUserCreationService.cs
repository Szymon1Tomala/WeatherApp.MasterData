namespace Domain.Interfaces.Users;

public interface IUserCreationService
{
    Task<string> CreateUserAsync(string firstName, string lastName, string email, string password);
}