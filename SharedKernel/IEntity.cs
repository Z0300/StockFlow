using System;
using System.Collections.Generic;
using System.Text;

namespace SharedKernel;

public interface IEntity
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();
    void ClearDomainEvents();
}
