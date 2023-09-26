using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Date { get; }
        Guid RequestId { get; }
    }
}
