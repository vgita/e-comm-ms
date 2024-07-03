using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DbExtensions
{
    public static async Task InitDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        await SeedDataAsync(context);
    }

    private static async Task SeedDataAsync(ApplicationDbContext context)
    {
        await SeedCustomersAsync(context);
        await SeedProductsAsync(context);
        await SeedOrdersAndItemsAsync(context);
    }

    private static async Task SeedCustomersAsync(ApplicationDbContext context)
    {
        if (await context.Customers.AnyAsync()) return;

        await context.Customers.AddRangeAsync(InitialData.Customers);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProductsAsync(ApplicationDbContext context)
    {
        if (await context.Products.AnyAsync()) return;

        await context.Products.AddRangeAsync(InitialData.Products);
        await context.SaveChangesAsync();
    }

    private static async Task SeedOrdersAndItemsAsync(ApplicationDbContext context)
    {
        if (await context.Orders.AnyAsync()) return;

        await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
        await context.SaveChangesAsync();
    }
}