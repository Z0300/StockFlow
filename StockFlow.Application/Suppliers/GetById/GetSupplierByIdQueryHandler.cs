using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Shared;
using StockFlow.Domain.Suppliers;

namespace StockFlow.Application.Suppliers.GetById;

internal sealed class GetSupplierByIdQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetSupplierByIdQuery, SupplierResponse>
{
    public async Task<Result<SupplierResponse>> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
    {
        SupplierResponse? supplier = await context.Suppliers
            .AsNoTracking()
            .Where(s => s.Id == query.SupplierId)
            .Select(supplier => new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                ContactInfo = supplier.ContactInfo
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (supplier is null)
        {
            return Result.Failure<SupplierResponse>(SupplierErrors.NotFound(query.SupplierId));
        }

        return supplier;
    }


}

