﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Attribute;
using TekhneCafe.Core.Exceptions.Attribute;
using TekhneCafe.DataAccess.Abstract;
using ProductAttributes = TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class AttributeManager : IAttributeService
    {
        private readonly IAttributeDal _attributeDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public AttributeManager(IAttributeDal attributeDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _attributeDal = attributeDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }


        public async Task CreateAttributeAsync(AttributeAddDto attributeAddDto)
        {
            ProductAttributes.Attribute attribute = _mapper.Map<ProductAttributes.Attribute>(attributeAddDto);
            await _attributeDal.AddAsync(attribute);

        }
        public List<AttributeListDto> GetAllAttributeAsync()
        {
            var attribute = _attributeDal.GetAll();
            return _mapper.Map<List<AttributeListDto>>(attribute);
        }

        public async Task<ProductAttributes.Attribute> GetAttributeByIdAsync(string id)
        {
            ProductAttributes.Attribute attribute = await _attributeDal.GetByIdAsync(Guid.Parse(id));
            if (attribute is null)
                throw new AttributeNotFoundException();

            return attribute;
        }


    }
}
