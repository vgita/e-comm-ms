namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{

    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        OrderId orderId = OrderId.Of(command.Order.Id);
        Order? order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken)
            ?? throw new OrderNotFoundException(command.Order.Id);

        UpdateOrder(order, command.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(order.Id.Value);
    }

    public void UpdateOrder(Order order, OrderDto orderDto)
    {
        Address updatedShippingAddress = Address.Of(
            orderDto.ShippingAddress.FirstName,
            orderDto.ShippingAddress.LastName,
            orderDto.ShippingAddress.EmailAddress,
            orderDto.ShippingAddress.AddressLine,
            orderDto.ShippingAddress.Country,
            orderDto.ShippingAddress.State,
            orderDto.ShippingAddress.ZipCode
        );

        Address updatedBillingAddress = Address.Of(
            orderDto.BillingAddress.FirstName,
            orderDto.BillingAddress.LastName,
            orderDto.BillingAddress.EmailAddress,
            orderDto.BillingAddress.AddressLine,
            orderDto.BillingAddress.Country,
            orderDto.BillingAddress.State,
            orderDto.BillingAddress.ZipCode
        );

        Payment updatedPayment = Payment.Of(
            orderDto.Payment.CardName,
            orderDto.Payment.CardNumber,
            orderDto.Payment.Expiration,
            orderDto.Payment.Cvv,
            orderDto.Payment.PaymentMethod
        );

        order.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            status: orderDto.Status);
    }

}
