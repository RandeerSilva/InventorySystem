using Inventory.Application.Abstractions.Persistence;
using Inventory.Domain.Entities;

namespace Inventory.Infrastructure.Repositories
{
    public class ProductRepository(InventoryDbContext context) : IProductRepository
    {
        public async Task AddAsync(Product product, CancellationToken ct)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync(ct);
        }
    }
}
