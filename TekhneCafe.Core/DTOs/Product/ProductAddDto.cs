﻿namespace TekhneCafe.Core.DTOs.Product
{
    public class ProductAddDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        //public virtual ICollection<ProductAttributeAddDto>? ProductAttributes { get; set; }
    }
}
