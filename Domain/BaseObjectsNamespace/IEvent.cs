using System;

namespace onion_architecture.Domain.BaseObjectsNamespace
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Date { get; }
    }
}
