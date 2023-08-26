﻿using Microsoft.AspNetCore.Mvc;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.Product;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product details.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Product not found</response>
        /// <response code="500">Server error</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productAddDto">The DTO containing the product details to create.</param>
        /// <returns>A message indicating the success of the creation.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct(ProductAddDto productAddDto)
        {
            await _productService.CreateProductAsync(productAddDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>The list of all products.</returns>
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A message indicating the success of the deletion.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {

            await _productService.DeleteProductAsync(id);
            return Ok();

        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="productUpdateDto">The DTO containing the updated product data.</param>
        /// <returns>A message indicating the success of the update.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDto productUpdateDto)
        {
            await _productService.UpdateProductAsync(productUpdateDto);
            return Ok();
        }
    }
}
