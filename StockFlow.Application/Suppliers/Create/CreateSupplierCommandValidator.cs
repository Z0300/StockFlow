using FluentValidation;

namespace StockFlow.Application.Suppliers.Create;

internal sealed class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
{
    public CreateSupplierCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty();
        RuleFor(command => command.ContactInfo)
            .NotEmpty();
    }
}
