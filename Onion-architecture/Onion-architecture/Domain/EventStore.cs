using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class EventStore
    {
        List<IEvent> Events;

        public EventStore()
        {
            Events = new List<IEvent>();
        }

        public void Add(IEvent eventToAdd)
        {
            Events.Add(eventToAdd);
        }

        public void RemoveEventsByRequestId(Guid requestId)
        {
            Events.RemoveAll(e => (e is RequestRejectEvent || e is RequestApprovedEvent || e is RequestCreateEvent) && e.RequestId == requestId);
        }
    }
}
