using System.Text;
using Domain.Interfaces.Users;
using Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services.Users;

public class UserCreationService(UserManager<User> userManager) : IUserCreationService
{
    public async Task<string> CreateUserAsync(string firstName, string lastName, string email, string password)
    {
        var doesUserExist = await userManager.FindByEmailAsync(email)
            .ContinueWith(x => x.Result is not null);

        if (doesUserExist)
        {
            throw new ArgumentException($"Can't create user with email: {email}\nUser with this email already exists");    
        }
        
        var user = new User(firstName, lastName)
        {
            UserName = email,
            Email = email,
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            return "User created successfully";
        }
        
        var stringBuilder = new StringBuilder();

        foreach (var error in result.Errors)
        {
            stringBuilder.AppendLine($"{error.Code}: {error.Description}");
        }

        return stringBuilder.ToString();
    }
}