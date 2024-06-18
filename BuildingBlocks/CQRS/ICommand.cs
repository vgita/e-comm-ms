using MediatR;

namespace BuildingBlocks.CQRS;

//empty command, does not produce a response
public interface ICommand : ICommand<Unit>
{
}

//produces a response
public interface ICommand<out TResponse> : IRequest<TResponse>
{

}
