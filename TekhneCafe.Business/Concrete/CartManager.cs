using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Cart;
using TekhneCafe.Core.Exceptions.Cart;
using TekhneCafe.Core.Filters.Cart;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartDal _cartDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public CartManager(ICartDal cartDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _cartDal = cartDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task CreateCartAsync(CartAddDto cartAddDto)
        {
            Cart cart = _mapper.Map<Cart>(cartAddDto);
            await _cartDal.AddAsync(cart);
        }

        public async Task DeleteCartAsync(string id)
        {
            Cart cart = await GetCartById(id);
            await _cartDal.SafeDeleteAsync(cart);
        }

        public List<CartListDto> GetAllCarts(CartRequestFilter filters = null)
        {
            var filteredResult = new CartFilterService().FilterCarts(_cartDal.GetAll(), filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<CartListDto>>(filteredResult.ResponseValue);
        }

        public async Task<CartListDto> GetCartByIdAsync(string id)
        {
            var cart = await GetCartById(id);
            return _mapper.Map<CartListDto>(cart);
        }

        public async Task UpdateCartAsync(CartUpdateDto cartUpdateDto)
        {
            Cart cart = await GetCartById(cartUpdateDto.Id);
            _mapper.Map(cartUpdateDto, cart);
            await _cartDal.UpdateAsync(cart);
        }
        private async Task<Cart> GetCartById(string id)
        {
            Cart cart = await _cartDal.GetByIdAsync(Guid.Parse(id));
            if (cart is null)
                throw new CartNotFoundException();

            return cart;
        }
    }
}

