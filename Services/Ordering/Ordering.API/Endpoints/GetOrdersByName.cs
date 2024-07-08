using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints;

//public record GetOrdersByNameRequest(string Name);
public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{name}", async (string name, ISender sender) =>
        {
            GetOrdersByNameQuery query = new(name);
            GetOrdersByNameResult result = await sender.Send(query);
            GetOrdersByNameResponse response = result.Adapt<GetOrdersByNameResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Gets orders by name.")
        .WithDescription("Gets orders by name.");
    }
}
