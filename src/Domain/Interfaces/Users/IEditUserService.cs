namespace Domain.Interfaces.Users;

public interface IEditUserService
{
    Task<string> EditUserAsync(string id, string firstName, string lastName);
}