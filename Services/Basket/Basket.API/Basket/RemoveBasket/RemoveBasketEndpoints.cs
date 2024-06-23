
namespace Basket.API.Basket.RemoveBasket;

//public record RemoveBasketRequest(string UserName) : IRequest<RemoveBasketResponse>;
public record RemoveBasketResponse(bool IsSuccess);

public class RemoveBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (ISender sender, string userName) =>
        {
            RemoveBasketCommand command = new(userName);
            RemoveBasketResult result = await sender.Send(command);
            RemoveBasketResponse response = result.Adapt<RemoveBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("RemoveBasket")
        .Produces<RemoveBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Remove basket")
        .WithDescription("Removes the basket for the specified user");
    }
}
