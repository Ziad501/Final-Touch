﻿using System.ComponentModel.DataAnnotations;

namespace FinalTouch.Api.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public int QuantityInStock { get; set; }
    }
}

