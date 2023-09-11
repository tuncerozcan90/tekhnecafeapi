﻿using TekhneCafe.Core.DTOs.ProductAttribute;

namespace TekhneCafe.Core.DTOs.Product
{
    public class ProductListDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? CategoryId { get; set; }
        public ICollection<ProductAttributeListDto>? ProductAttributes { get; set; }
    }
}
