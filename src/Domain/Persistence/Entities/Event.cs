namespace Domain.Persistence.Entities;

public enum EventStatus
{
    Pending,
    Published
}

public class Event
{
    public Guid Id { get; init; }
    public EventStatus Status { get; set; }
    public string Content { get; set; }
    public DateTimeOffset CreatedOn { get; init; }
    public DateTimeOffset? PublishedOn { get; set; }
}