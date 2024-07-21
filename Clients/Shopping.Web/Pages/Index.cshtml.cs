namespace Shopping.Web.Pages;

public class IndexModel(ICatalogService catalogService,
    IBasketService basketService,
    ILogger<IndexModel> logger) : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = [];


    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Index page visited");

        var result = await catalogService.GetProducts();
        ProductList = result.Products;
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Add to cart button clicked");

        GetProductByIdResponse productResponse = await catalogService.GetProduct(productId);

        var basket = await basketService.LoadUserBasket();

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Quantity = 1,
            Price = productResponse.Product.Price,
            Color = "Black",
        });

        await basketService.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToPage("Cart"); ;
    }

}
