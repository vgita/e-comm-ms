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

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
            .WithMessage("Id is required.");
        RuleFor(x => x.Product.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters.");
        RuleFor(x => x.Product.Price).GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}

internal class UpdateProductCommandHandler
    (IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
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
