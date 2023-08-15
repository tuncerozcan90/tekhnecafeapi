using AutoMapper;
using TekhneCafe.Core.DTOs.CartLineProductAttribute;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Mappers
{
    public class CartLineProductAttributeProfile : Profile
    {
        public CartLineProductAttributeProfile()
        {
            CreateMap<CartLineProductAttributeAddDto, CartLineProductAttribute>();
        }
    }
}
