using FluentValidation;

namespace StockFlow.Application.Suppliers.Delete;

internal sealed class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
{
    public DeleteSupplierCommandValidator()
    {
        RuleFor(command => command.SupplierId)
            .NotEmpty();
    }
}
