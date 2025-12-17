using Inventory.Application.Abstractions.Persistence;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    public class CategoryRepository(InventoryDbContext context) : ICategoryRepository
    {
        public Task<bool> ExistsAsync(int categoryId, CancellationToken ct)
            => context.Categories.AnyAsync(c => c.Id == categoryId, ct);

        public async Task AddAsync(Category category, CancellationToken ct)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync(ct);
        }
    }
}
