using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.DTOs.Attribute;


namespace TekhneCafe.Business.Mappers
{
    public class AttributeProfile : Profile
    {
        public AttributeProfile()
        {
            CreateMap<AttributeAddDto ,TekhneCafe.Entity.Concrete.Attribute>();
            CreateMap<AttributeUpdateDto ,TekhneCafe.Entity.Concrete.Attribute>();
           
            CreateMap<TekhneCafe.Entity.Concrete.Attribute, AttributeListDto>();
        }
    }
}
