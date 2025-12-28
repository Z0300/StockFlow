using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Suppliers;

public class Supplier : Entity<SupplierId>
{
    private Supplier(
        SupplierId id,
        string name,
        string contactInfo) : base(id)
    {
        Name = name;
        ContactInfo = contactInfo;
    }
    public string Name { get; private set; }
    public string ContactInfo { get; private set; }

    protected Supplier() { }

    public static Supplier Create(string name, string contactInfo)
        => new(SupplierId.New(), name, contactInfo);

    public void ChangeName(string name)
      => Name = name;

    public void ChangeContactInfo(string contactInfo)
        => ContactInfo = contactInfo;
}
