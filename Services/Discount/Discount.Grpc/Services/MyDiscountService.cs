using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class MyDiscountService(
    DiscountContext dbContext,
    ILogger<MyDiscountService> logger
) : DiscountService.DiscountServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName, context.CancellationToken);

        coupon ??= new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        CouponModel couponModel = new()
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };

        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        if (request.Coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
        }

        Coupon coupon = new()
        {
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = request.Coupon.Amount
        };

        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync(context.CancellationToken);

        logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

        CouponModel couponModel = new()
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };

        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        if (request.Coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
        }

        Coupon? coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.Coupon.ProductName} is not found."));

        coupon.ProductName = request.Coupon.ProductName;
        coupon.Description = request.Coupon.Description;
        coupon.Amount = request.Coupon.Amount;

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync(context.CancellationToken);

        logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

        CouponModel couponModel = new()
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };

        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        Coupon? coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(x => x.Id == request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound, $"Discount with Id={request.Id} is not found."));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync(context.CancellationToken);

        logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", coupon.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }
}
