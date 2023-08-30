﻿using TekhneCafe.Core.DTOs.ProductAttribute;

namespace TekhneCafe.Core.DTOs.Product
{
    public class ProductUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public ICollection<ProductAttributeAddDto> ProductAttributes { get; set; }
    }
}
