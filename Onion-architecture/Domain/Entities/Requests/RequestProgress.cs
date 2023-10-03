using System;
using Onion_architecture.Domain.BaseObjectsNamespace;
using Onion_architecture.Domain.Entities.Workflows;

namespace Onion_architecture.Domain.Entities.Requests
{
    public class RequestProgress
    {
        public Guid RequestId { get; private set; }
        public int CurrentStep { get; private set; }
        public bool IsApproved { get; private set; }
        public bool IsRejected { get; private set; }
        public IEvent CurrentEvent { get; private set; }

        public RequestProgress(Guid requestId)
        {
            RequestId = requestId;
            CurrentStep = 0;
            IsApproved = false;
            IsRejected = false;
        }

        public IEvent AdvanceStep(WorkflowStep currentStep, int totalSteps)
        {
            CurrentStep++;
            currentStep.UpdateComment($"Approved step {CurrentStep}");

            if (currentStep != null)
            {
                if(CurrentStep == totalSteps)
                {
                    CurrentEvent = new RequestApprovedEvent(RequestId);
                    Approve();
                }
            }
            else
            {
                throw new InvalidOperationException("No next step is available");
            }

            return CurrentEvent;
        }

        public void Approve()
        {
            IsApproved = true;
        }

        public void Reject()
        {
            IsRejected = true;
        }
    }
}
