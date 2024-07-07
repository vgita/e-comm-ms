namespace Ordering.Application.Orders.Commands.RemoveOrder;

public record RemoveOrderCommand(Guid Id) : ICommand<RemoveOrderResult>;

public record RemoveOrderResult(bool IsSuccess);

public class RemoveOrderCommandValidator : AbstractValidator<RemoveOrderCommand>
{
    public RemoveOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
