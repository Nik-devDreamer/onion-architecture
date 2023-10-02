using System;

namespace Onion_architecture.Domain.Entities.Request
{
    public class RequestCreateEvent : IEvent.IEvent
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public Guid RequestId { get; private set; }

        public RequestCreateEvent(Guid requestId)
        {
            RequestId = requestId;
            Date = DateTime.UtcNow;
        }
    }
}
