using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface ICartService
    {
        Task<Cart> GetCartByIdAsync(Guid cartId);
        Task<List<Cart>> GetAllCartsAsync();
        Task CreateCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(Guid cartId);
    }
}
