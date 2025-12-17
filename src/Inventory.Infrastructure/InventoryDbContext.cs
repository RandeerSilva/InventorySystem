using Microsoft.EntityFrameworkCore;
using Inventory.Application.Abstractions.Persistence;
using Inventory.Domain.Entities;

namespace Inventory.Infrastructure
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : DbContext(options), IInventoryDbContext
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
    }
}
