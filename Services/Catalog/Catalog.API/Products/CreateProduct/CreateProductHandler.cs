using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        return new CreateProductResult(product.Id);
    }
}
