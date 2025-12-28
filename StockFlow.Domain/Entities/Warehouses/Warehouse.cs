using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Warehouses;

public class Warehouse : Entity<WarehouseId>
{
    private Warehouse(
        WarehouseId id,
        string name,
        string location
        ) : base(id)
    {
        Id = id;
        Name = name;
        Location = location;
    }

    protected Warehouse() { }

    public string Name { get; set; }
    public string Location { get; set; }

    public static Warehouse Create(string name, string location)
        => new(WarehouseId.New(), name, location);

    public void ChangeName(string name)
        => Name = name;

    public void ChangeLocation(string location)
        => Location = location;
}
