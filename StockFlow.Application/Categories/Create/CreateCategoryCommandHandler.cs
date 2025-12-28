using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Exceptions;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Categories;

namespace StockFlow.Application.Categories.Create;

internal sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (await _categoryRepository.IsNameUnique(request.Name, cancellationToken))
        {
            return Result.Failure<Guid>(CategoryErrors.NameNotUnique);
        }

        try
        {
            var category = Category.Create(
                request.Name,
                request.Description
            );

            _categoryRepository.Add(category);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.Id.Value;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(CategoryErrors.NameNotUnique);
        }

    }
}
