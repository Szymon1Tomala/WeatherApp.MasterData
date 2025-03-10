namespace Domain.Persistence.Entities;

public class City
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid CountryId { get; init; }
}