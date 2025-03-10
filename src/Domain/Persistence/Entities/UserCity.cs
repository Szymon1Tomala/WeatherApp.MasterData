namespace Domain1.Persistence.Entities;

public class UserCity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CityId { get; set; }
}