using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Shared;

namespace StockFlow.Application.Categories.Get;

public sealed record GetCategoryQuery : IQuery<List<CategoryResponse>>;
