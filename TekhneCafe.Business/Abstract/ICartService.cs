using TekhneCafe.Core.DTOs.Cart;
using TekhneCafe.Core.Filters.Cart;

namespace TekhneCafe.Business.Abstract
{
    public interface ICartService
    {
        Task<CartListDto> GetCartByIdAsync(string id);
        List<CartListDto> GetAllCarts(CartRequestFilter filters = null);
        Task CreateCartAsync(CartAddDto cartAddDto);
        Task UpdateCartAsync(CartUpdateDto cartUpdateDto);
        Task DeleteCartAsync(string id);
    }
}
