
namespace Basket.API.Exceptions;

public class BasketNotFoundException(string UserName)
    : NotFoundException(nameof(ShoppingCart), UserName)
{
}
