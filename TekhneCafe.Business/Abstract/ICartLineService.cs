using TekhneCafe.Core.DTOs.CartLine;

namespace TekhneCafe.Business.Abstract
{
    public interface ICartLineService
    {
        Task AddCartLineAsync(CartLineAddDto cartLineAddDto);
        Task UpdateCartLineAsync(CartLineUpdateDto cartLineUpdateDto);
        Task DeleteCartLineAsync(string id);
    }
}
