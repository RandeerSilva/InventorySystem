using Microsoft.EntityFrameworkCore;
using Inventory.Domain.Entities;

namespace Inventory.Application.Abstractions.Persistence
{
    public interface IInventoryDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Product> Products { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
