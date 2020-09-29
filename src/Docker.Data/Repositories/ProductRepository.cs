using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.Data.Entities;

namespace Docker.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DockerDbContext _dbContext;

        public ProductRepository(DockerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }
    }
}
