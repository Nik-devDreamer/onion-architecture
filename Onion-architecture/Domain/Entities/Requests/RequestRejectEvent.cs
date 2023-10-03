using System;
using Onion_architecture.Domain.BaseObjectsNamespace;

namespace Onion_architecture.Domain.Entities.Requests
{
    public class RequestRejectEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public Guid RequestId { get; private set; }

        public RequestRejectEvent(Guid requestId)
        {
            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            RequestId = requestId;
        }
    }
}
