using TekhneCafe.Core.DTOs.Attribute;

namespace TekhneCafe.Business.Abstract
{
    public interface IAttributeService
    {
        Task CreateAttributeAsync(AttributeAddDto attributeAddDto);
        List<AttributeListDto> GetAllAttributeAsync();
        Task<TekhneCafe.Entity.Concrete.Attribute> GetAttributeByIdAsync(string id);
    }
}
