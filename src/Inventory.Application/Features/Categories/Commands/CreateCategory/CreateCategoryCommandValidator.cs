using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator
        : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}
