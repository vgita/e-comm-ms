namespace Basket.API.Basket.RemoveBasket;

public record RemoveBasketCommand(string UserName) : ICommand<RemoveBasketResult>;
public record RemoveBasketResult(bool IsSuccess);

public class RemoveBasketCommandValidator : AbstractValidator<RemoveBasketCommand>
{
    public RemoveBasketCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("Username is required");
    }
}

public class RemoveBasketCommandHandler : ICommandHandler<RemoveBasketCommand, RemoveBasketResult>
{
    public Task<RemoveBasketResult> Handle(RemoveBasketCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(new RemoveBasketResult(true));
    }
}