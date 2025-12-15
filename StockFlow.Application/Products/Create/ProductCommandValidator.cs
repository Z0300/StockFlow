using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace StockFlow.Application.Products.Create;

internal sealed class ProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public ProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Sku).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.WarehouseId).NotEmpty();
    }
}
