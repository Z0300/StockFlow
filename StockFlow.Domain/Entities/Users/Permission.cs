namespace StockFlow.Domain.Entities.Users;

public sealed class Permission
{
    public static readonly Permission Admin = new(1, "users:full_access");
    public Permission(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; }
}
