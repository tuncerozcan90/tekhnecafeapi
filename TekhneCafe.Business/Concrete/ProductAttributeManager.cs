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

        public Task<List<ProductAttributeListDto>> GetProductAttributesByProductId(string ProductId)
        {
            throw new NotImplementedException();
        }

        //public async Task<List<ProductAttributeListDto>> GetProductAttributeByProductId(string ProductId)
        //{
        //    var productAttribute = await _productAttributeDal.GetByIdAsync(Guid.Parse(ProductId));
        //    return _mapper.Map<ProductAttributeListDto>(productAttribute);
        //}

        //public async Task<List<ProductAttributeListDto>> GetProductAttributesByProductId(string ProductId)
        //{
        //    // ProductId'yi Guid'e çevirin ve bu ID'ye sahip ürünün attributelerini alın
        //    var productIdGuid = Guid.Parse(ProductId);

        //    // ProductAttributeListDto'ya dönüştürün ve listeyi döndürün
        //    return _mapper.Map<List<ProductAttributeListDto>>(productAttributes);
        //}
    }
}
