namespace Shopping.Web.Models.Catalog;

public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<string> Categories { get; set; } = [];
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public decimal Price { get; set; }
}

//wrapper records
public record GetProductsResponse(IEnumerable<ProductModel> Products);
public record GetProductByIdResponse(ProductModel Product);
