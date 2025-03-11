using System.Text;
using Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services;

public class UserService(UserManager<User> userManager)
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
    
    public async Task<string> ChangeUserPasswordAsync(string id, string currentPassword, string newPassword)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user is null)
        {
            throw new ArgumentException($"User with id: {id} was not found");    
        }

        var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (result.Succeeded)
        {
            return "Password changed successfully";
        }
        
        var stringBuilder = new StringBuilder();

        foreach (var error in result.Errors)
        {
            stringBuilder.AppendLine($"{error.Code}: {error.Description}");
        }

        return stringBuilder.ToString();
    }
    
    public async Task<string> ChangeUserEmailAsync(string id, string newEmail)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user is null)
        {
            throw new ArgumentException($"User with id: {id} was not found");    
        }

        var result = await userManager.ChangeEmailAsync(user, newEmail, string.Empty); // to do

        if (result.Succeeded)
        {
            return "Email changed successfully";
        }
        
        var stringBuilder = new StringBuilder();

        foreach (var error in result.Errors)
        {
            stringBuilder.AppendLine($"{error.Code}: {error.Description}");
        }

        return stringBuilder.ToString();
    }
    
    public async Task<string> EditUserAsync(string id, string firstName, string lastName)
    {
        var user = await userManager.FindByIdAsync(id);

        if (user is null)
        {
            throw new ArgumentException($"User with id: {id} was not found");    
        }

        user.UpdateFirstName(firstName);
        user.UpdateLastName(lastName);

        return "User edited successfully";
    }
}