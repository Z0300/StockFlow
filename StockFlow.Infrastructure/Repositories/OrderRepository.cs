using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Orders.Enums;
using StockFlow.Domain.Entities.Products;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class OrderRepository : Repository<Order, OrderId>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
