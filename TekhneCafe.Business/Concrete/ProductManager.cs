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

            // Update product properties from the DTO
            _mapper.Map(productUpdateDto, product);

            // Update product attributes
            var newProductAttributeIds = productUpdateDto.ProductAttributes.Select(attr => attr.AttributeId).ToList();
            var existingAttributeIds = product.ProductAttributes.Select(attr => attr.AttributeId.ToString()).ToList();

            // Remove attributes that are no longer associated
            var attributesToRemove = product.ProductAttributes.Where(attr => !newProductAttributeIds.Contains(attr.AttributeId.ToString())).ToList();
            foreach (var attribute in attributesToRemove)
                product.ProductAttributes.Remove(attribute);

            // Add new attributes
            var attributesToAdd = newProductAttributeIds.Except(existingAttributeIds);
            foreach (var attributeId in attributesToAdd)
            {
                var newAttr = productUpdateDto.ProductAttributes.First(_ => _.AttributeId == attributeId);
                product.ProductAttributes.Add(new ProductAttribute { IsRequired = newAttr.IsRequired, Price = 32 });
            }

            // Save changes to the product and attributes
            await _productDal.UpdateAsync(product);
        }
    }
}
