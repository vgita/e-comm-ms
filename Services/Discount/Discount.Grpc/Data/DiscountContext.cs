using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options)
    : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon
            {
                Id = 1,
                ProductName = "Keyboard",
                Description = "Keyboard Discount",
                Amount = 20
            },
            new Coupon
            {
                Id = 2,
                ProductName = "Mouse",
                Description = "Mouse Discount",
                Amount = 15
            }
        );
    }
}
