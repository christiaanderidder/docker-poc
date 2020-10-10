using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docker.Data.Entities;

namespace Docker.Web.Models
{
    public class ProductEditViewModel
    {
        public ProductEditViewModel() { }
        public ProductEditViewModel(Product entity)
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

        public void ApplyToEntity(Product entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.Price = Price;
            entity.Stock = Stock;
        }
    }
}
