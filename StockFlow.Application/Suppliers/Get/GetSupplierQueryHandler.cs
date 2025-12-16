using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Shared;

namespace StockFlow.Application.Suppliers.Get;

internal sealed class GetSupplierQueryHandler(IApplicationDbContext context)
    : IQueryHandler<GetSupplierQuery, List<SupplierResponse>>
{
    public async Task<Result<List<SupplierResponse>>> Handle(GetSupplierQuery query, CancellationToken cancellationToken)
    {
        List<SupplierResponse> suppliers = await context.Suppliers
            .AsNoTracking()
            .Select(supplier => new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                ContactInfo = supplier.ContactInfo
            })
            .ToListAsync(cancellationToken);

        return suppliers;
    }
}
