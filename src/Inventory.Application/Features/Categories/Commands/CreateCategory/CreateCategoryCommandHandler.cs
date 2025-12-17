using Inventory.Application.Abstractions.Persistence;
using Inventory.Domain.Entities;
using MediatR;

namespace Inventory.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ICategoryRepository repository)
        : IRequestHandler<CreateCategoryCommand, int>
    {
        public async Task<int> Handle(
            CreateCategoryCommand request,
            CancellationToken ct)
        {
            var category = new Category(request.Name);

            await repository.AddAsync(category, ct);

            return category.Id;
        }
    }
}
