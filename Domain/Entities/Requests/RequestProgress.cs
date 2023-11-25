using System;
using onion_architecture.Domain.Entities.Requests.Events;
using onion_architecture.Domain.BaseObjectsNamespace;

namespace onion_architecture.Domain.Entities.Requests
{
    public class RequestProgress
    {
        public Guid RequestId { get; private set; }
        public int CurrentStep { get; private set; }
        public bool IsApproved { get; private set; }
        public bool IsRejected { get; private set; }

        private Workflow _workflow;
        
        public RequestProgress(Guid requestId,  Workflow workflow)
        {
            RequestId = requestId;
            _workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
            CurrentStep = 0;
            IsApproved = false;
            IsRejected = false;
        }

        public void AdvanceStep(WorkflowStep currentStep)
        {
            CurrentStep++;
            currentStep.UpdateComment($"Approved step {CurrentStep}");
        }

        public void Approve(Guid? userId)
        {
            if (userId != _workflow.Steps[CurrentStep].UserId)
            {
                throw new InvalidOperationException("User does not have permission to perform this action.");
            }
            
            IsApproved = true;
        }

        public void Reject(Guid? userId)
        {
            if (userId != _workflow.Steps[CurrentStep].UserId)
            {
                throw new InvalidOperationException("User does not have permission to perform this action.");
            }
            
            IsRejected = true;
        }
    }
}