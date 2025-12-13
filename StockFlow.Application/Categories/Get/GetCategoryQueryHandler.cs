using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;

namespace StockFlow.Application.Categories.Get;

internal sealed class GetCategoryQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetCategoryQuery, List<CategoryResponse>>
{
    public async Task<Result<List<CategoryResponse>>> Handle(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        List<CategoryResponse> categories = await context.Categories
            .Select(category => new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            })
            .ToListAsync(cancellationToken);

        return categories;
    }
}
