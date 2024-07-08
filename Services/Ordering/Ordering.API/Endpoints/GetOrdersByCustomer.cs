
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

//public record GetOrdersByCustomerRequest(Guid CustomerId);
public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{customerId}/orders", async (Guid customerId, ISender sender) =>
        {
            GetOrdersByCustomerQuery query = new(customerId);
            GetOrdersByCustomerResult result = await sender.Send(query);
            GetOrdersByCustomerResponse response = result.Adapt<GetOrdersByCustomerResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get orders by customer")
        .WithDescription("Gets orders by customer.");
    }
}
