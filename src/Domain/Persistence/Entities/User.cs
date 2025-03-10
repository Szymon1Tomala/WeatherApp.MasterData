using Microsoft.AspNetCore.Identity;

namespace Domain.Persistence.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}