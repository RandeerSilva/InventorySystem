using Inventory.Domain.Entities;

namespace Inventory.Application.Abstractions.Persistence
{
    public interface ICategoryRepository
    {
        Task<bool> ExistsAsync(int categoryId, CancellationToken ct);
        Task AddAsync(Category category, CancellationToken ct);
    }
}
