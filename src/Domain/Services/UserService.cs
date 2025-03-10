
using Domain1.Persistence;
using Domain1.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain1.Services;

public class UserService(DatabaseContext databaseContext)
{
    public Guid CreateUser(string firstName, string lastName, string email, string password)
    {
        return default;
    }
}