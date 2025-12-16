using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Categories.Delete;

public sealed record DeleteCategoryCommand(Guid Id) : ICommand;
