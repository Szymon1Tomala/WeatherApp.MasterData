using Domain1.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain1.Persistence.Configurations;

public class UserCityConfiguration : IEntityTypeConfiguration<UserCity>
{
    public void Configure(EntityTypeBuilder<UserCity> builder)
    {
        builder.ToTable("UserCity");
        
        builder.HasKey(x => x.Id);
        builder.HasOne<User>().WithMany().HasForeignKey(uc => uc.UserId);
        builder.HasOne<City>().WithMany().HasForeignKey(uc => uc.CityId);
    }
}