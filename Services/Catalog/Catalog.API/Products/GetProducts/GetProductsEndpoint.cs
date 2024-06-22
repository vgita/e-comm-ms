namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(string? Category);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, [AsParameters] GetProductsRequest request) =>
        {
            GetProductsQuery query = request.Adapt<GetProductsQuery>();
            GetProductsResult result = await sender.Send(query);
            GetProductsResponse response = result.Adapt<GetProductsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get products")
        .WithDescription("Gets all products in the catalog");
    }
}
