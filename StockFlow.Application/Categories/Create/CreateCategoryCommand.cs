using StockFlow.Application.Abstractions.Messaging;


namespace StockFlow.Application.Categories.Create;

public sealed record CreateCategoryCommand(string Name, string Description) : ICommand<Guid>;

