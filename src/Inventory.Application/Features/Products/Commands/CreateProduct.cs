using MediatR;

namespace Inventory.Application.Features.Products.Commands
{
    public record CreateProductCommand(
        string Name,
        decimal Price,
        int CategoryId) : IRequest<int>;
}
