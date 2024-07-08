using Ordering.Application.Orders.Commands.RemoveOrder;

namespace Ordering.API.Endpoints;

//public record RemoveOrderRequest(Guid Id);
public record RemoveOrderResponse(bool IsSuccess);

public class RemoveOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
        {
            RemoveOrderCommand command = new(id);
            RemoveOrderResult result = await sender.Send(command);
            RemoveOrderResponse response = result.Adapt<RemoveOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("RemoveOrder")
        .Produces<RemoveOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Removes an existing order.")
        .WithDescription("Removes an existing order.");
    }
}
