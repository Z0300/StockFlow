using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace StockFlow.Application.Warehouses.Update;

internal sealed class UpdateWarehouseCommandValidator : AbstractValidator<UpdateWarehouseCommand>
{
    public UpdateWarehouseCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}
