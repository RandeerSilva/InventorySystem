using FluentValidation;

namespace Inventory.Application.Features.Products.Commands
{
    public class CreateProductCommandValidator
        : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.CategoryId).GreaterThan(0);
        }
    }
}
