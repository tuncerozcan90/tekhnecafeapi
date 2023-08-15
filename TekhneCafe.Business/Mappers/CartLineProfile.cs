using AutoMapper;
using TekhneCafe.Core.DTOs.CartLine;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Mappers
{
    public class CartLineProfile : Profile
    {
        public CartLineProfile()
        {
            CreateMap<CartLineAddDto, CartLine>();
        }
    }
}
