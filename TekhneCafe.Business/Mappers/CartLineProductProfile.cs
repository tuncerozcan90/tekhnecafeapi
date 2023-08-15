using AutoMapper;
using TekhneCafe.Core.DTOs.CartLineProduct;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Mappers
{
    public class CartLineProductProfile : Profile
    {
        public CartLineProductProfile()
        {
            CreateMap<CartLineProductAddDto, CartLineProduct>();
        }
    }
}
