using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Domain.Entities.Products;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class ProductRepository : Repository<Product, ProductId>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext
           .Set<Product>()
           .AnyAsync(product => product.Name == name, cancellationToken);
    }
}
