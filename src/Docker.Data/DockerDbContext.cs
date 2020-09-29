using Docker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Docker.Data
{
    public class DockerDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Database");
    }
}
