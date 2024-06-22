namespace Catalog.API.Products.RemoveProduct;

//public record RemoveProductRequest(Guid Id) : IRequest<RemoveProductResponse>;
public record RemoveProductResponse(Guid Id);

public class RemoveProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            RemoveProductResult response = await sender.Send(new RemoveProductCommand(id));

            return Results.Ok(response.Adapt<RemoveProductResponse>());
        })
        .WithName("RemoveProduct")
        .Produces<RemoveProductResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Remove product")
        .WithDescription("Removes a product from the catalog");
    }
}
