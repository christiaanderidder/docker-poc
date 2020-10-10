using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Docker.Api.Models
{
    public class Product
    {
        public Product(Data.Entities.Product entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            Price = entity.Price;
            Stock = entity.Stock;
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
