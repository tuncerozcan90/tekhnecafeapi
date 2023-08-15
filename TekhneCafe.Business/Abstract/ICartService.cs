using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface ICartService
    {
        float GetTotalPriceOfCart(Cart cart);
        Task<Cart> GetValidCart(Cart cart);
    }
}
