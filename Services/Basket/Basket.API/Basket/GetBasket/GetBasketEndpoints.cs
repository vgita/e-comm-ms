namespace Basket.API.Basket.GetBasket;

//public record GetBasketRequest(string UserName) : IRequest<BasketResponse>;
public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (ISender sender, string userName) =>
        {
            GetBasketQuery query = new(userName);
            GetBasketResult result = await sender.Send(query);
            GetBasketResponse response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get basket")
        .WithDescription("Gets the basket for the specified user");
    }
}
