
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(Guid Id);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest request, ISender send) =>
        {
            UpdateOrderCommand command = request.Adapt<UpdateOrderCommand>();
            UpdateOrderResult result = await send.Send(command);
            UpdateOrderResponse response = result.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Updates an existing order.")
        .WithDescription("Updates an existing order.");
    }
}
