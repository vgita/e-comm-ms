
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(string? Category) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Product> products = session.Query<Product>();

        if (query.Category is not null)
            products = products.Where(p => p.Categories.Contains(query.Category));

        return new GetProductsResult(await products.ToListAsync(cancellationToken));
    }
}
