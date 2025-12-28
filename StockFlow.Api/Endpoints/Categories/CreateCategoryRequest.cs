using System.ComponentModel.DataAnnotations;

namespace StockFlow.Api.Endpoints.Categories;

public record CreateCategoryRequest(string Name, string Description);
