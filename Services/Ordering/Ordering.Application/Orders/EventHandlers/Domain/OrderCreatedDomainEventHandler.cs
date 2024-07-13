using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedDomainEventHandler
    (IPublishEndpoint publishEndpoint,
    IFeatureManager featureManager,
    ILogger<OrderCreatedDomainEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("OrderCreatedEvent: {OrderId} is successfully handled.", domainEvent.Order.Id);

        if (await featureManager.IsEnabledAsync("OrderFulfillment"))
        {
            OrderDto orderDto = domainEvent.Order.ToOrderDto();
            await publishEndpoint.Publish(orderDto, cancellationToken);
        }
    }
}
