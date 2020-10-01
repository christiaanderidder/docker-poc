using Docker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Docker.Data
{
    public class DockerDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DockerDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(_configuration.GetConnectionString("Database"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Product 1", Description = "First product", Price = 10, CreatedAt = DateTimeOffset.Now, UpdatedAt = DateTimeOffset.Now },
                new Product { Id = 2, Name = "Product 2", Description = "Second product", Price = 15, CreatedAt = DateTimeOffset.Now, UpdatedAt = DateTimeOffset.Now },
                new Product { Id = 3, Name = "Product 3", Description = "Third product", Price = 20, CreatedAt = DateTimeOffset.Now, UpdatedAt = DateTimeOffset.Now }
            );
        }

        public void Initialize()
        {
            Database.Migrate();
        }
    }
}
