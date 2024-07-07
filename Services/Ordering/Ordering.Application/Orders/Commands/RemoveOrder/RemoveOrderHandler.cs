
namespace Ordering.Application.Orders.Commands.RemoveOrder;

public class RemoveOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<RemoveOrderCommand, RemoveOrderResult>
{
    public async Task<RemoveOrderResult> Handle(RemoveOrderCommand command, CancellationToken cancellationToken)
    {
        OrderId orderId = OrderId.Of(command.Id);
        Order? order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken)
            ?? throw new OrderNotFoundException(command.Id);

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new RemoveOrderResult(true);
    }
}
