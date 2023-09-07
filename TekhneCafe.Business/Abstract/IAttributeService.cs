﻿using TekhneCafe.Core.DTOs.Attribute;

namespace TekhneCafe.Business.Abstract
{
    public interface IAttributeService
    {
        Task CreateAttributeAsync(AttributeAddDto attributeAddDto);
        Task DeleteAttributeAsync(string id);
        List<AttributeListDto> GetAllAttribute();
        Task<AttributeListDto> GetAttributeByIdAsync(string id);
        Task UpdateAttributeAsync(AttributeUpdateDto attributeUpdateDto);
    }
}
