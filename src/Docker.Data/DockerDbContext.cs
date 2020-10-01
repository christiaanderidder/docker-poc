using Docker.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

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

        public override int SaveChanges()
        {
            SetTimestamps();

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetTimestamps();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTimestamps();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SetTimestamps();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken); 
        }

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

        private void SetTimestamps()
        {
            var trackedEntries = ChangeTracker.Entries();
            var newEntries = trackedEntries.Where(e => e.State == EntityState.Added).Select(e => e.Entity).OfType<BaseEntity>().Cast<BaseEntity>();
            var updatedEntries = trackedEntries.Where(e => e.State == EntityState.Modified).Select(e => e.Entity).OfType<BaseEntity>().Cast<BaseEntity>();

            foreach (var newEntry in newEntries)
            {
                if (newEntry == null) continue;

                newEntry.CreatedAt = DateTimeOffset.Now;
                newEntry.UpdatedAt = DateTimeOffset.Now;
            }

            foreach (var updatedEntry in updatedEntries)
            {
                if (updatedEntry == null) continue;

                updatedEntry.UpdatedAt = DateTimeOffset.Now;
            }
        }
    }
}
