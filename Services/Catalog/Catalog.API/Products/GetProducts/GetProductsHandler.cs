

namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery(string? Category, int? Page = 1, int? PageSize = 10)
    : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Product> productsQuery = session.Query<Product>();

        if (query.Category is not null)
            productsQuery = productsQuery.Where(p => p.Categories.Contains(query.Category));

        return new GetProductsResult
            (await productsQuery.ToPagedListAsync
            (query.Page!.Value, query.PageSize!.Value, cancellationToken));
    }
}
