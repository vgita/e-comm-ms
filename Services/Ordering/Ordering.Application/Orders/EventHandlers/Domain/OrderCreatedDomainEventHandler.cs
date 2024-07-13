using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedDomainEventHandler
    (IPublishEndpoint publishEndpoint,
    ILogger<OrderCreatedDomainEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("OrderCreatedEvent: {OrderId} is successfully handled.", domainEvent.Order.Id);

        OrderDto orderDto = domainEvent.Order.ToOrderDto();

        await publishEndpoint.Publish(orderDto, cancellationToken);
    }
}
