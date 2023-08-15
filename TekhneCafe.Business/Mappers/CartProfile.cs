using AutoMapper;
using TekhneCafe.Core.DTOs.Cart;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Mappers
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartAddDto, Cart>();
        }
    }
}
