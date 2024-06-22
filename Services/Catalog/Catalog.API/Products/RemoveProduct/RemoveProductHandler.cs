
namespace Catalog.API.Products.RemoveProduct;

public record RemoveProductCommand(Guid Id) : ICommand<RemoveProductResult>;
public record RemoveProductResult(Guid Id);

public class RemoveProductCommandHandler
    (IDocumentSession session) :
    IRequestHandler<RemoveProductCommand, RemoveProductResult>
{
    public async Task<RemoveProductResult> Handle(RemoveProductCommand command, CancellationToken cancellationToken)
    {
        Product? product = await session.LoadAsync<Product>(command.Id, cancellationToken)
            ?? throw new ProductNotFoundException(command.Id);

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new RemoveProductResult(product.Id);
    }
}
