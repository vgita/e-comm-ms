
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(string? Category) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler
    (IDocumentSession session, ILogger<GetProductsQuery> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsQueryHandler.Handle called with query: {@Query}", query);

        IQueryable<Product> products = session.Query<Product>();

        if (query.Category is not null)
            products = products.Where(p => p.Categories.Contains(query.Category));

        return new GetProductsResult(await products.ToListAsync(cancellationToken));
    }
}
