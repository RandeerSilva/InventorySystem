using Inventory.Application.Abstractions.Persistence;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Products.Commands
{
    public class CreateProductCommandHandler(
        ICategoryRepository categoryRepo,
        IProductRepository productRepo)
        : IRequestHandler<CreateProductCommand, int>
    {
        public async Task<int> Handle(
            CreateProductCommand request,
            CancellationToken ct)
        {
            var exists = await categoryRepo.ExistsAsync(request.CategoryId, ct);

            if (!exists)
                throw new Exception("Category not found");

            var product = new Product(
                request.Name,
                request.Price,
                request.CategoryId);

            await productRepo.AddAsync(product, ct);

            return product.Id;
        }
    }
}
