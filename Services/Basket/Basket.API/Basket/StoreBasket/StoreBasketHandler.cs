using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(ShoppingCart Cart);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart)
            .NotNull()
            .WithMessage("Cart cannot be null");
        RuleFor(x => x.Cart.UserName)
            .NotEmpty()
            .WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler
(IBasketRepository repository,
DiscountService.DiscountServiceClient discountServiceClient)
: ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        await Task.WhenAll(cart.Items.Select(item => DeductDiscount(item, cancellationToken)));

        await repository.StoreBasket(cart, cancellationToken);
        return new StoreBasketResult(cart);
    }

    private async Task DeductDiscount(ShoppingCartItem item, CancellationToken cancellationToken)
    {
        CouponModel coupon = await discountServiceClient
            .GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.ProductName },
                cancellationToken: cancellationToken);
        item.Price -= coupon.Amount;
    }
}
