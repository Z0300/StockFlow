using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Categories.Delete;

public sealed record DeleteCategoryCommand(Guid Id) : ICommand;
