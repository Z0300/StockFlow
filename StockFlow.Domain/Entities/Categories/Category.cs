using System.Security.Cryptography;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Categories;

public class Category : Entity<CategoryId>
{
    private Category(
        CategoryId id,
        string name,
        string description
        ) : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    protected Category() { }

    public string Name { get; private set; }
    public string Description { get; private set; }


    public static Category Create(string name, string description)
        => new(CategoryId.New(), name, description);

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}
