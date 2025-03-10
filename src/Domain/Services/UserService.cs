
using Domain.Persistence;
using Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Services;

public class UserService(DatabaseContext databaseContext)
{
    public Guid CreateUser(string firstName, string lastName, string email, string password)
    {
        return default;
    }
}