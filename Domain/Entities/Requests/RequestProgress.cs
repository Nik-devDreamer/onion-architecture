using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DomainTests")]
namespace Domain.Entities.Requests
{
    public class RequestProgress
    {
        public Guid RequestId { get; private set; }
        public int CurrentStep { get; private set; }
        public bool IsApproved { get; private set; }
        public bool IsRejected { get; private set; }

        private readonly Workflow _workflow;
        
        public RequestProgress(Guid requestId,  Workflow workflow)
        {
            RequestId = requestId;
            _workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
            CurrentStep = 0;
            IsApproved = false;
            IsRejected = false;
        }

        public void AdvanceStep(WorkflowStep currentStep, Guid? userId)
        {
            if (CurrentStep == _workflow.Steps.Count - 1)
            {
                Approve(userId);
            }

            CurrentStep++;
            currentStep.UpdateComment($"Approved step {CurrentStep}");
        }

        internal void Approve(Guid? userId)
        {
            if (userId != _workflow.Steps[CurrentStep].UserId)
            {
                throw new InvalidOperationException("User does not have permission to perform this action.");
            }
            
            IsApproved = true;
        }

        internal void Reject(Guid? userId)
        {
            if (userId != _workflow.Steps[CurrentStep].UserId)
            {
                throw new InvalidOperationException("User does not have permission to perform this action.");
            }
            
            IsRejected = true;
        }
    }
}