using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class CartLineManager : ICartLineService
    {
        private readonly ICartLineDal _cartLineDal;

        public CartLineManager(ICartLineDal cartLineDal)
        {
            _cartLineDal = cartLineDal;
        }

        public async Task<CartLine> GetByIdAsync(Guid id)
        {
            return await _cartLineDal.GetByIdAsync(id);
        }

        public async Task AddCartLineAsync(CartLine cartLine)
        {
            await _cartLineDal.AddAsync(cartLine);
        }

        public async Task UpdateCartLineAsync(CartLine cartLine)
        {
            await _cartLineDal.UpdateAsync(cartLine);
        }

        public async Task DeleteCartLineAsync(Guid cartLineId)
        {
            var cartLine = await _cartLineDal.GetByIdAsync(cartLineId);
            if (cartLine != null)
            {
                await _cartLineDal.HardDeleteAsync(cartLine);
            }
        }
    }
}
