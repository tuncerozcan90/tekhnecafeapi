using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.Product;
using TekhneCafe.Core.Exceptions.Product;
using TekhneCafe.Core.Filters.Product;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        private readonly IProductAttributeDal _productAttributeDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;


        public ProductManager(IProductDal productDal, IMapper mapper, IHttpContextAccessor httpContext, IProductAttributeDal productAttributeDal)
        {
            _productDal = productDal;
            _mapper = mapper;
            _httpContext = httpContext;
            _productAttributeDal = productAttributeDal;
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
                await _productAttributeDal.HardDeleteAsync(item);
            }
            await _productDal.SafeDeleteAsync(product);
        }

        public List<ProductListDto> GetAllProducts(ProductRequestFilter filter)
        {
            var filteredResult = FilterProducts(filter);
            return _mapper.Map<List<ProductListDto>>(filteredResult.ResponseValue);
        }

        public async Task<ProductDetailDto> GetProductByIdAsync(string id)
        {
            Product product = await _productDal.GetProductIncludeAllAsync(id);
            ThrowErrorIfProductNotFound(product);
            return _mapper.Map<ProductDetailDto>(product);
        }

        private ProductResponseFilter<List<Product>> FilterProducts(ProductRequestFilter filter)
        {
            var query = GetProducts();
            var filteredResult = new ProductFilterService().FilterProducts(query, filter);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return filteredResult;
        }

        private IQueryable<Product> GetProducts()
         => _productDal.GetAll(_ => !_.IsDeleted).Include(_ => _.ProductAttributes).ThenInclude(_ => _.Attribute).Include(_ => _.Category).AsNoTracking()
               .AsSingleQuery();

        public List<ProductListDto> GetProductsByCategory(string categoryId)
        {
            var products = _productDal.GetProductsByCategory(categoryId);
            return _mapper.Map<List<ProductListDto>>(products);
        }

        private static void ThrowErrorIfProductNotFound(Product product)
        {
            if (product is null)
                throw new ProductNotFoundException();
        }

        public async Task UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            Product product = await _productDal.GetProductIncludeAttributeAsync(productUpdateDto.Id.ToLower());

            ThrowErrorIfProductNotFound(product);
            product.ModifiedDate = DateTime.Now;
            product.Name = productUpdateDto.Name;
            product.Description = productUpdateDto.Description;
            product.Price = productUpdateDto.Price;
            product.CategoryId = Guid.Parse(productUpdateDto.CategoryId);

            // Update product attributes
            UpdateProductAttribute(productUpdateDto, product);

            // Save changes to the product and attributes
            await _productDal.UpdateAsync(product);
        }



        private void UpdateProductAttribute(ProductUpdateDto productUpdateDto, Product product)
        {
            var newProductAttributeIds = productUpdateDto.ProductAttributes?.Select(attr => attr.AttributeId.ToLower()).ToList();
            var existingAttributeIds = product.ProductAttributes.Select(attr => attr.AttributeId.ToString()).ToList();
            RemoveAttributesFromProduct(newProductAttributeIds, product);
            UpdateAttributesOfProduct(existingAttributeIds, newProductAttributeIds, product, productUpdateDto);
            AddNewAttributesToProduct(existingAttributeIds, newProductAttributeIds, product, productUpdateDto);
        }



        private void RemoveAttributesFromProduct(List<string> newProductAttributeIds, Product product)
        {
            // Remove attributes that are no longer associated
            var attributesToRemove = product.ProductAttributes?.Where(attr => { return !(newProductAttributeIds != null && newProductAttributeIds.Contains(attr.AttributeId.ToString())); }).ToList();
            if (attributesToRemove != null)
                foreach (var attribute in attributesToRemove)
                    product.ProductAttributes.Remove(attribute);
        }



        private void UpdateAttributesOfProduct(List<string> existingAttributeIds, List<string> newProductAttributeIds, Product product, ProductUpdateDto productUpdateDto)
        {
            // Remove attributes that are no longer associated
            IEnumerable<string> attributesToUpdate = null;
            if (existingAttributeIds != null)
                attributesToUpdate = newProductAttributeIds?.Where(attr => existingAttributeIds.Contains(attr));
            if (attributesToUpdate != null)
                foreach (var attributeId in attributesToUpdate)
                {
                    var updatedAttr = productUpdateDto.ProductAttributes?.First(_ => _.AttributeId.ToLower() == attributeId.ToLower());
                    var existingAttr = product.ProductAttributes?.First(_ => _.AttributeId == Guid.Parse(attributeId));
                    if (updatedAttr != null && existingAttr != null)
                        _mapper.Map(updatedAttr, existingAttr);
                }
        }



        private void AddNewAttributesToProduct(List<string> existingAttributeIds, List<string> newProductAttributeIds, Product product, ProductUpdateDto productUpdateDto)
        {
            // Add new attributes
            if (existingAttributeIds is null)
                return;
            var attributesToAdd = newProductAttributeIds?.Except(existingAttributeIds).ToList();
            if (attributesToAdd != null)
                foreach (var attributeId in attributesToAdd)
                {
                    var newAttr = productUpdateDto.ProductAttributes?.First(_ => _.AttributeId.ToLower() == attributeId.ToLower());
                    product.ProductAttributes?.Add(new ProductAttribute { IsRequired = newAttr.IsRequired, Price = newAttr.Price, AttributeId = Guid.Parse(newAttr.AttributeId) });
                }
        }

    }
}
