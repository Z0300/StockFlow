using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;
using StockFlow.Domain.Categories;

namespace StockFlow.Application.Categories.GetById;

internal sealed class GetCategoryByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetCategoryByIdQuery, CategoryResponse>
{
    public async Task<Result<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        CategoryResponse? category = await context.Categories
             .AsNoTracking()
             .Where(category => category.Id == query.Id)
             .Select(category => new CategoryResponse
             {
                 Id = category.Id,
                 Name = category.Name,
                 Description = category.Description
             })
             .SingleOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound(query.Id));
        }

        return category;
    }
}
