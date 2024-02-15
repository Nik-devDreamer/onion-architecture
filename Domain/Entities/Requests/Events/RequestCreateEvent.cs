using Domain.BaseObjectsNamespace;

namespace Domain.Entities.Requests.Events
{
    public class RequestCreateEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public Guid RequestId { get; private set; }

        public RequestCreateEvent(Guid requestId)
        {
            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            RequestId = requestId;
        }
    }
}
