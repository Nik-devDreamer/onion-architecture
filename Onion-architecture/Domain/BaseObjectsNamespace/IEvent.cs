using System;

namespace Onion_architecture.Domain.BaseObjectsNamespace
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Date { get; }
    }
}
