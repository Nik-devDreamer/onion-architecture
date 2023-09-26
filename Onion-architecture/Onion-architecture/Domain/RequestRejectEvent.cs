using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class RequestRejectEvent : IEvent
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public Guid RequestId { get; private set; }

        public RequestRejectEvent(Guid requestId)
        {
            RequestId = requestId;
            Date = DateTime.UtcNow;
        }
    }
}
