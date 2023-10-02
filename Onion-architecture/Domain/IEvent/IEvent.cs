using System;

namespace Onion_architecture.Domain.IEvent
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Date { get; }
    }
}
