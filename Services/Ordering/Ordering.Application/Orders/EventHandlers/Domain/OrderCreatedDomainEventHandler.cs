namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedDomainEventHandler(ILogger<OrderCreatedDomainEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("OrderCreatedEvent: {OrderId} is successfully handled.", notification.Order.Id);
        return Task.CompletedTask;
    }
}
