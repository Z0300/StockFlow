using FluentValidation;

namespace StockFlow.Application.Products.Update;

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Sku).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.CategoryId).NotEmpty().NotEqual(Guid.Empty);
    }
}
