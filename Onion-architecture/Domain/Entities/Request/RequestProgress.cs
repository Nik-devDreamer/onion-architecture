using System;
using Onion_architecture.Domain.Entities.Workflow;

namespace Onion_architecture.Domain.Entities.Request
{
    public class RequestProgress
    {
        public Guid RequestId { get; private set; }
        public int CurrentStep { get; private set; }
        public bool IsApproved { get; private set; }
        public bool IsRejected { get; private set; }
        public string Comment { get; private set; }

        public RequestProgress(Guid requestId)
        {
            RequestId = requestId;
            CurrentStep = 0;
            IsApproved = false;
            IsRejected = false;
        }

        public void AdvanceStep(WorkflowStep currentStep)
        {
            CurrentStep++;
            currentStep.UpdateComment($"Approved step {CurrentStep}");
        }

        public void Approve()
        {
            IsApproved = true;
            Comment = "Approved";
        }

        public void Reject()
        {
            IsRejected = true;
            Comment = "Rejected";
        }
    }
}
