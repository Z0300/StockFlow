using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Users.Events;

public sealed record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
