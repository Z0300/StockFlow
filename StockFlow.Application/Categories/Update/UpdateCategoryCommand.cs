using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Categories.Update;

public sealed record UpdateCategoryCommand(Guid Id, string Name, string Description) : ICommand;

