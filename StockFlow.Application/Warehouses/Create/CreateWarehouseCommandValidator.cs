using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace StockFlow.Application.Warehouses.Create;

internal sealed class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
