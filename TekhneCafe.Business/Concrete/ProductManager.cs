using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
            Product product = await _productDal.GetProductIncludeAllAsync(id);
            ThrowErrorIfProductNotFound(product);
            product.IsDeleted = true;
            foreach (var item in product.ProductAttributes)
            {
                item.IsDeleted = true;
            }
            await _productDal.SafeDeleteAsync(product);
        }

        public List<ProductListDto> GetAllProducts()
        {
            var product = _productDal.GetAll(_ => !_.IsDeleted).Include(_ => _.ProductAttributes).ThenInclude(_ => _.Attribute).Include(_ => _.Category).ToList();
            return _mapper.Map<List<ProductListDto>>(product);
        }

        public async Task<ProductListDto> GetProductByIdAsync(string id)
        {
            Product product = await _productDal.GetProductIncludeAllAsync(id);
            ThrowErrorIfProductNotFound(product);
            return _mapper.Map<ProductListDto>(product);
        }

        public  List<ProductListDto> GetProductsByCategory(string categoryId)
        {
            var products =  _productDal.GetProductsByCategory(categoryId);
            return _mapper.Map<List<ProductListDto>>(products);
        }

        private static void ThrowErrorIfProductNotFound(Product product)
        {
            if (product is null)
                throw new ProductNotFoundException();
        }

        public async Task UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            Product product = await _productDal.GetProductIncludeAllAsync(productUpdateDto.Id.ToLower());

            ThrowErrorIfProductNotFound(product);

            // Update product attributes
            UpdateProductAttribute(productUpdateDto, product);

            // Update product properties from the DTO
            //_mapper.Map(productUpdateDto, product);
            product.Name = productUpdateDto.Name;
            product.Description = productUpdateDto.Description;
            product.Price = productUpdateDto.Price;

            // Save changes to the product and attributes
            await _productDal.UpdateAsync(product);
        }

        private void UpdateProductAttribute(ProductUpdateDto productUpdateDto, Product product)
        {
            var newProductAttributeIds = productUpdateDto.ProductAttributes.Select(attr => attr.Attribute.Id.ToLower()).ToList();
            var existingAttributeIds = product.ProductAttributes.Select(attr => attr.AttributeId.ToString()).ToList();

            // Remove attributes that are no longer associated
            var attributesToRemove = product.ProductAttributes.Where(attr => !newProductAttributeIds.Contains(attr.AttributeId.ToString())).ToList();
            foreach (var attribute in attributesToRemove)
                product.ProductAttributes.Remove(attribute);

            // Update attributes
            var attributesToUpdate = newProductAttributeIds.Where(attr => existingAttributeIds.Contains(attr));
            foreach (var attributeId in attributesToUpdate)
            {
                var updatedAttr = productUpdateDto.ProductAttributes.First(_ => _.Attribute.Id.ToLower() == attributeId.ToLower());
                var existingAttr = product.ProductAttributes.First(_ => _.AttributeId == Guid.Parse(attributeId));
                _mapper.Map(updatedAttr, existingAttr);
            }

            // Add new attributes
            var attributesToAdd = newProductAttributeIds.Except(existingAttributeIds).ToList();
            foreach (var attributeId in attributesToAdd)
            {
                var newAttr = productUpdateDto.ProductAttributes.First(_ => _.Attribute.Id.ToLower() == attributeId.ToLower());
                product.ProductAttributes.Add(new ProductAttribute { IsRequired = newAttr.IsRequired, Price = newAttr.Price, AttributeId = Guid.Parse(newAttr.Attribute.Id) });
            }
        }
    }
}
