using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Data.Entities
{
    public class Product : SoftDeleteBaseEntity
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
