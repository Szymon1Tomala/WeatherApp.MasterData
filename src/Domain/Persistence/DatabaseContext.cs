using Domain.Persistence.Configurations;
using Domain.Persistence.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserCity> UserCities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName is not null && tableName.StartsWith("AspNet"))
            {
                entityType.SetSchema("identity");
            }
        }
        
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserCityConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
    }
}