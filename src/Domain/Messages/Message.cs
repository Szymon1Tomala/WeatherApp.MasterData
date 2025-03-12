namespace Domain.Messages;

public abstract record Message
{
    public static Guid Id => Guid.NewGuid();
    public static DateTimeOffset TimeStamp => DateTimeOffset.UtcNow;
}