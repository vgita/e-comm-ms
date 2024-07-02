namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);
        // Value object conversion
        builder.Property(x => x.Id)
            .HasConversion(cId => cId.Value,
                dbId => OrderItemId.Of(dbId));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        builder.Property(x => x.Quantity).IsRequired();
        
        builder.Property(x => x.Price).IsRequired();
    }
}
