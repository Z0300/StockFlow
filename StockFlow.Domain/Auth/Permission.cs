using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Auth;

public sealed class Permission
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
