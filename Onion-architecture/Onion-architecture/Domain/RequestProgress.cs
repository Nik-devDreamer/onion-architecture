using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class RequestProgress
    {
        public Guid RequestId { get; private set; }
        public int CurrentStep { get; private set; }
        public bool IsApproved { get; private set; }

        public RequestProgress(Guid requestId)
        {
            RequestId = requestId;
            CurrentStep = 0;
            IsApproved = false;
        }

        public void AdvanceStep()
        {
            CurrentStep++;
        }

        public void Approve()
        {
            IsApproved = true;
        }
    }
}
