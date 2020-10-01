using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.Data.Entities;
using Microsoft.Extensions.Logging;

namespace Docker.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DockerDbContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(DockerDbContext dbContext, ILogger<ProductRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IList<Product> GetAll()
        {
            return _dbContext.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _dbContext.Products.Find(id);
        }

        public bool Persist(Product product)
        {
            try
            { 
                _ = _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Failed to perist product");
                return false;
            }
        }
    }
}
