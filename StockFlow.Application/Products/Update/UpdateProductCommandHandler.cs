using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Shared;

namespace StockFlow.Application.Products.Update;

internal sealed class UpdateProductCommandHandler
    : ICommandHandler<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateProductCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        product.ChangeName(request.Name);
        product.ChangeSku(request.Sku);
        product.ChangePrice(new Money(request.Price, Currency.Php));
        product.ChangeCategory(new CategoryId(request.CategoryId!.Value));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
