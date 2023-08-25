using AutoMapper;
using Microsoft.AspNetCore.Http;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Product;
using TekhneCafe.Core.Exceptions.Product;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public ProductManager(IProductDal productDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _productDal = productDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task CreateProductAsync(ProductAddDto productAddDto)
        {
            Product product = _mapper.Map<Product>(productAddDto);
            await _productDal.AddAsync(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            Product product = await _productDal.GetByIdAsync(Guid.Parse(id));
            ThrowErrorIfProductNotFound(product);
            product.IsDeleted = true;
            await _productDal.SafeDeleteAsync(product);
        }

        public List<ProductListDto> GetAllProducts()
        {
            var product = _productDal.GetAll(_ => !_.IsDeleted);
            return _mapper.Map<List<ProductListDto>>(product);
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            Product product = await _productDal.GetByIdAsync(Guid.Parse(id));
            ThrowErrorIfProductNotFound(product);
            return product;
        }

        private static void ThrowErrorIfProductNotFound(Product product)
        {
            if (product is null)
                throw new ProductNotFoundException();
        }

        public async Task UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            Product product = await _productDal.GetByIdAsync(Guid.Parse(productUpdateDto.Id));
            ThrowErrorIfProductNotFound(product);
            _mapper.Map(productUpdateDto, product);
            _productDal.UpdateAsync(product);
        }
    }
}
