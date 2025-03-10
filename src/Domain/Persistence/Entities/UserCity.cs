namespace Domain.Persistence.Entities;

public class UserCity
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid CityId { get; set; }
}