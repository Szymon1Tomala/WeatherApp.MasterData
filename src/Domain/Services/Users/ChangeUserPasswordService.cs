using System.Text;
using Domain.Interfaces.Users;
using Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services.Users;

public class ChangeUserPasswordService(UserManager<User> userManager) : IChangeUserPasswordService
{
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
}