using MediatR;

namespace Inventory.Application.Features.Categories.Commands.CreateCategory
{

    public record CreateCategoryCommand(string Name) : IRequest<int>;
}
