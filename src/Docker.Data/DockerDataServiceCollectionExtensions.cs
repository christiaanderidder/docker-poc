using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docker.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Docker.Data
{
    public static class DockerDataServiceCollectionExtensions
    {
        public static void AddDockerData(this IServiceCollection services)
        {
            services.AddDbContext<DockerDbContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
