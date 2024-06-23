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

public class StoreBasketCommandHandler()
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        // var basket = await _repository.UpdateBasket(command.Cart);
        // return new StoreBasketResult(basket != null);
        return Task.FromResult(new StoreBasketResult(cart));
    }
}
