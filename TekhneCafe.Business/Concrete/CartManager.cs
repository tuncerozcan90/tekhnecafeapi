using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;

        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }
        public async Task<Cart> GetCartByIdAsync(Guid cartId)
        {
            return await _cartDal.GetByIdAsync(cartId);
        }

        public async Task<List<Cart>> GetAllCartsAsync()
        {
            return await _cartDal.GetAll().ToListAsync();
        }

        public async Task CreateCartAsync(Cart cart)
        {
            await _cartDal.AddAsync(cart);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            await _cartDal.UpdateAsync(cart);
        }

        public async Task DeleteCartAsync(Guid cartId)
        {
            var cart = await _cartDal.GetByIdAsync(cartId);
            if (cart != null)
            {
                await _cartDal.HardDeleteAsync(cart);
            }
        }
    }
}

