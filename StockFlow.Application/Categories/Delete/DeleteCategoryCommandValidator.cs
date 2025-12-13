using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace StockFlow.Application.Categories.Delete;

internal sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
