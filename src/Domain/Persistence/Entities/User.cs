using Microsoft.AspNetCore.Identity;

namespace Domain.Persistence.Entities;

public class User(string firstName, string lastName) : IdentityUser
{
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;

    public void UpdateFirstName(string firstName)
    {
        if (FirstName != firstName)
        {
            FirstName = firstName;
        }
    }
    
    public void UpdateLastName(string lastName)
    {
        if (LastName != lastName)
        {
            LastName = lastName;
        }
    }
}