
namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(string UserName, List<ShoppingCartItem> Items) : IRequest<StoreBasketResponse>;
public record StoreBasketResponse(ShoppingCart Cart);

public class StoreBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (ISender sender, StoreBasketRequest request) =>
        {
            StoreBasketCommand command = request.Adapt<StoreBasketCommand>();
            StoreBasketResult result = await sender.Send(command);
            StoreBasketResponse response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{request.UserName}", response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store basket")
        .WithDescription("Stores the basket for the specified user");
    }
}
