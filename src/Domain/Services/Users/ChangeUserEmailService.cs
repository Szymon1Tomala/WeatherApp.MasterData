using System.Text;
using Domain.Interfaces.Users;
using Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services.Users;

public class ChangeUserEmailService(UserManager<User> userManager) : IChangeUserEmailService
{
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
}