using TekhneCafe.Core.DTOs.CartLine;

namespace TekhneCafe.Business.Abstract
{
    public interface ICartLineService
    {
        Task AddCartLineAsync(CartLineAddDto cartLineAddDto);
        Task DeleteCartLineAsync(string id);
    }
}
