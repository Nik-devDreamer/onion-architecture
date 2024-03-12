using System.Runtime.CompilerServices;
using Domain.Entities.Users;

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

        public void AdvanceStep(WorkflowStep currentStep,  User user)
        {
            IsAuthorized(user);
            
            if (CurrentStep == _workflow.Steps.Count - 1)
            {
                Approve(user);
            }

            CurrentStep++;
            currentStep.UpdateComment($"Approved step {CurrentStep}");
        }

        internal void Approve(User user)
        {
            IsAuthorized(user);
            
            IsApproved = true;
        }

        internal void Reject(User user)
        {
            IsAuthorized(user);
            
            IsRejected = true;
        }
        
        private void IsAuthorized(User user)
        {
            if (user.Id != _workflow.Steps.ElementAt(CurrentStep).UserId && user.RoleId != _workflow.Steps.ElementAt(CurrentStep).RoleId)
            {
                throw new InvalidOperationException("User does not have permission to perform this action.");
            }
        }
    }
}