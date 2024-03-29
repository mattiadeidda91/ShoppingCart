﻿using System.ComponentModel;

namespace ShoppingCart.Abstractions.Dapper.Entities
{
    public class Product
    {
        [Description("ignore")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
    }
}
