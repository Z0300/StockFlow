using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Domain.Shared;

namespace StockFlow.Domain.Entities.Products;

public class Product : Entity<ProductId>
{
    private Product(
        ProductId id,
        string name,
        string sku,
        Money price,
        CategoryId categoryId) : base(id)
    {
        Name = name;
        Sku = sku;
        Price = price;
        CategoryId = categoryId;
    }

    protected Product() { }

    public string Name { get; private set; }
    public string Sku { get; private set; }
    public Money Price { get; private set; }
    public CategoryId? CategoryId { get; private set; }

    public static Product Create(
        string name,
        string sku,
        Money price,
        CategoryId categoryId)
        => new(ProductId.New(), name, sku, price, categoryId);

    public void ChangeName(string name)
        => Name = name;
    public void ChangeSku(string sku)
        => Sku = sku;
    public void ChangePrice(Money price)
        => Price = price;
    public void ChangeCategory(CategoryId categoryId)
        => CategoryId = categoryId;

}
