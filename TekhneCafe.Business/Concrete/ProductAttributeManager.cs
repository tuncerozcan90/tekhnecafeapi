using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.ProductAttribute;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class ProductAttributeManager : IProductAttributeService
    {
        private readonly IProductAttributeDal _productAttributeDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public ProductAttributeManager(IProductAttributeDal productAttributeDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _productAttributeDal = productAttributeDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task CreateProductAttributeAsync(ProductAttributeAddDto productAttributeAddDto)
        {
            ProductAttribute productAttribute = _mapper.Map<ProductAttribute>(productAttributeAddDto);
            await _productAttributeDal.AddAsync(productAttribute);
        }

        public async Task<ProductAttributeListDto> GetProductAttributeById(string id)
        {
            var productAttribute = await _productAttributeDal.GetByIdAsync(Guid.Parse(id));
            return _mapper.Map<ProductAttributeListDto>(productAttribute);
        }
    }
}
