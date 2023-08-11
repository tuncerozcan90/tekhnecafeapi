using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.DataAccess.Abstract;
using TekhneCafe.Core.DTOs.Cart;
using TekhneCafe.Core.Filters.AppRole;
using TekhneCafe.Core.Filters.Cart;
using TekhneCafe.Entity.Concrete;

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
