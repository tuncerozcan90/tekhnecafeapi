using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.CartLine;
using TekhneCafe.Core.Exceptions.CartLine;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class CartLineManager : ICartLineService
    {

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ICartLineDal _cartLineDal;

        public CartLineManager(ICartLineDal cartLineDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _cartLineDal = cartLineDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        //public async Task AddCartLineAsync(CartLine cartLine)
        //{
        //    await _cartLineDal.AddAsync(cartLine);
        //}

        //public async Task UpdateCartLineAsync(CartLine cartLine)
        //{
        //    await _cartLineDal.UpdateAsync(cartLine);
        //}

        //public async Task DeleteCartLineAsync(Guid cartLineId)
        //{
        //    var cartLine = await _cartLineDal.GetByIdAsync(cartLineId);
        //    if (cartLine != null)
        //    {
        //        await _cartLineDal.HardDeleteAsync(cartLine);
        //    }
        //}

        public async Task AddCartLineAsync(CartLineAddDto cartLineAddDto)
        {

            CartLine cartLine = _mapper.Map<CartLine>(cartLineAddDto);
            await _cartLineDal.AddAsync(cartLine);
        }

        public async Task DeleteCartLineAsync(string id)
        {
            CartLine cartLine = await GetCartLineById(id);
            await _cartLineDal.SafeDeleteAsync(cartLine);
        }
        private async Task<CartLine> GetCartLineById(string id)
        {
            CartLine cartLine = await _cartLineDal.GetByIdAsync(Guid.Parse(id));
            if (cartLine is null)
                throw new CartLineNotFoundException();

            return cartLine;
        }
    }

}
