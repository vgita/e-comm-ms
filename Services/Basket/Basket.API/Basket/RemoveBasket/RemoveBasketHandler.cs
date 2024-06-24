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

public class RemoveBasketCommandHandler
(IBasketRepository repository)
: ICommandHandler<RemoveBasketCommand, RemoveBasketResult>
{
    public async Task<RemoveBasketResult> Handle(RemoveBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.RemoveBasket(command.UserName, cancellationToken);
        return new RemoveBasketResult(true);
    }
}