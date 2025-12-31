using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Orders.Enums;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Suppliers;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class OrderRepository : Repository<Order, OrderId>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

}
