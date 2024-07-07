namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext)
 : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        List<Order> orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .OrderBy(o => o.OrderName.Value)
            .Skip(query.PaginationRequest.PageIndex * query.PaginationRequest.PageSize)
            .Take(query.PaginationRequest.PageSize)
            .ToListAsync(cancellationToken);

        long count = await dbContext.Orders
            .LongCountAsync(cancellationToken);

        return new GetOrdersResult(new PaginatedResult<OrderDto>(
            query.PaginationRequest.PageIndex,
            query.PaginationRequest.PageSize,
            count,
            orders.ToOrderDtos()));
    }
}