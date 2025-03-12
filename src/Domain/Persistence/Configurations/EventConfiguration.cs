using Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Persistence.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events", "outbox");
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Status).IsRequired();
        builder.Property(e => e.Content).IsRequired().HasMaxLength(2000);
        builder.Property(e => e.CreatedOn).IsRequired();
    }
}