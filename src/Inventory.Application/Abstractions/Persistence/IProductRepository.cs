using Inventory.Domain.Entities;

namespace Inventory.Application.Abstractions.Persistence
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, CancellationToken ct);
    }
}
