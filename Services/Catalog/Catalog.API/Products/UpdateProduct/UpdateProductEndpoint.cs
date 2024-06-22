
namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price);

public record UpdateProductResponse(Product Product);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async (Guid id, UpdateProductRequest request, ISender sender) =>
        {
            UpdateProductCommandProduct command = request.Adapt<UpdateProductCommandProduct>();
            UpdateProductResult result = await sender.Send(new UpdateProductCommand(id, command));
            UpdateProductResponse response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update product")
        .WithDescription("Updates a product in the catalog");
    }
}
