
namespace Catalog.API.Products.RemoveProduct;

public record RemoveProductCommand(Guid Id) : ICommand<RemoveProductResult>;
public record RemoveProductResult(Guid Id);

public class RemoveProductCommandHandler(
    IDocumentSession session,
    ILogger<RemoveProductCommandHandler> logger) :
    IRequestHandler<RemoveProductCommand, RemoveProductResult>
{
    public async Task<RemoveProductResult> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing product with ID {Id}", command.Id);

        Product? product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
        {
            logger.LogWarning("Product with ID {Id} not found", command.Id);
            throw new ProductNotFoundException(command.Id);
        }

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new RemoveProductResult(product.Id);
    }
}
