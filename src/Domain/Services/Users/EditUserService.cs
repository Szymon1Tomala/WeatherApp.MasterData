using Domain.Interfaces.Users;
using Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services.Users;

public class EditUserService(UserManager<User> userManager) : IEditUserService
{
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