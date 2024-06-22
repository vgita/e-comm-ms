namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, UpdateProductCommandProduct Product)
    : ICommand<UpdateProductResult>;

public record UpdateProductCommandProduct(
    string Name,
    List<string> Categories,
    string Description,
    string ImageFile,
    decimal Price)
    : ICommand<UpdateProductResult>;

public record UpdateProductResult(Product Product);

internal class UpdateProductCommandHandler(
    IDocumentSession session,
    ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle called with command: {@Command}", command);

        Product product = await session.LoadAsync<Product>(command.Id, cancellationToken)
            ?? throw new ProductNotFoundException(command.Id);

        UpdateProductCommandProduct commandProduct = command.Product;

        product.Name = commandProduct.Name;
        product.Categories = commandProduct.Categories;
        product.Description = commandProduct.Description;
        product.ImageFile = commandProduct.ImageFile;
        product.Price = commandProduct.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product);
    }
}
