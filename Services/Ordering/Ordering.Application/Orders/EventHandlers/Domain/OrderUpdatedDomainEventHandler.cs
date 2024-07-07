
namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderUpdatedDomainEventHandler(ILogger<OrderUpdatedDomainEventHandler> logger)
: INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("OrderUpdatedEvent: {OrderId} is successfully handled.", notification.Order.Id);
        return Task.CompletedTask;
    }
}
