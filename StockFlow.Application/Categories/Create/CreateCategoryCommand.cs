using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Application.Abstractions.Messaging;


namespace StockFlow.Application.Categories.Create;

public sealed record CreateCategoryCommand(string Name, string Description) : ICommand;

