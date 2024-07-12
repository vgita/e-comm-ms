namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent
{
    public Guid Id => Guid.NewGuid();
    public DateTime CreatedOn => DateTime.UtcNow;
    public string? EventType => GetType().AssemblyQualifiedName;
}
