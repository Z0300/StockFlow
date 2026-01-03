using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Exceptions;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Shared;

namespace StockFlow.Application.Products.Create;

internal sealed class CreateProductCommandHandler
    : ICommandHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await _productRepository.IsNameUnique(request.Name, cancellationToken))
        {
            return Result.Failure<Guid>(ProductErrors.NameNotUnique);
        }

        try
        {
            var product = Product.Create(
                request.Name,
                request.Sku,
                new Money(request.Price, Currency.Php),
                new CategoryId(request.CategoryId)
            );

            _productRepository.Add(product);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return product.Id.Value;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(ProductErrors.NameNotUnique);
        }
    }
}
