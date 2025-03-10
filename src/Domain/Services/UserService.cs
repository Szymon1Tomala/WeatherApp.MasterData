using Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services;

public class UserService(UserManager<User> userManager)
{
    public async Task<string> CreateUserAsync(string firstName, string lastName, string email, string password)
    {
        var user = new User
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return result.Errors.ToString();
        }

        return "User created successfully";
    }
}