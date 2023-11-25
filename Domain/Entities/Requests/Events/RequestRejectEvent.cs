using System;
using onion_architecture.Domain.BaseObjectsNamespace;

namespace onion_architecture.Domain.Entities.Requests.Events
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
