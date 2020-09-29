using System.Collections.Generic;
using Docker.Data.Entities;

namespace Docker.Data.Repositories
{
    public interface IProductRepository
    {
        IList<Product> GetAll();
    }
}