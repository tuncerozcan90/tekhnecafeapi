using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly ICartDal _cartDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IProductService _productService;
        private readonly ICartLineProductAttributeService _productAttributeService;

        public CartManager(ICartDal cartDal, IMapper mapper, IHttpContextAccessor httpContext, IProductService productService, ICartLineProductAttributeService productAttributeService)
        {
            _cartDal = cartDal;
            _mapper = mapper;
            _httpContext = httpContext;
            _productService = productService;
            _productAttributeService = productAttributeService;
        }

        public float GetTotalPriceOfCart(Cart cart)
        {
            float totalPrice = 0;
            foreach (var cartLine in cart.CartLines)
                foreach (var cartLineProduct in cartLine.CartLineProducts)
                {
                    totalPrice += cartLineProduct.Quantity * cartLineProduct.Price;
                    foreach (var cartLineProductAttribute in cartLineProduct.CartLineProductAttributes)
                        totalPrice += cartLineProductAttribute.Quantity * cartLineProductAttribute.Price;
                }

            return totalPrice;
        }

        public async Task<Cart> GetValidCart(Cart cart)
        {
            foreach (var cartLine in cart.CartLines)
                foreach (var cartLineProduct in cartLine.CartLineProducts)
                {
                    var product = await _productService.GetProductByIdAsync(cartLineProduct.ProductId.ToString());
                    if (product is null)
                    {
                        cartLine.CartLineProducts.Remove(cartLineProduct);
                        continue;
                    }
                    foreach (var cartLineProductAttribute in cartLineProduct.CartLineProductAttributes)
                    {
                        bool productAttributeExists = await _productAttributeService.CartLineProductAttributeExistsAsync(cartLineProductAttribute.Id.ToString());
                        if (!productAttributeExists)
                            cartLineProduct.CartLineProductAttributes.Remove(cartLineProductAttribute);
                    }
                }

            return new Cart();
        }
    }
}

