using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extensions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using DiscountContext context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        context.Database.MigrateAsync();
        return app;
    }

}
