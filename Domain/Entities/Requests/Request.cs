using System.Collections.ObjectModel;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests.Events;
using Domain.Entities.Users;

namespace Domain.Entities.Requests
{
    public class Request
    {
        private readonly List<IEvent> _events = new List<IEvent>();

        public Guid Id { get; private set; }
        public User User { get; private set; }
        public Document Document { get; private set; }
        public Workflow Workflow { get; private set; }
        public RequestProgress Progress { get; private set; }
        public IReadOnlyCollection<IEvent> EventsList => _events.AsReadOnly();

        public Request(Guid id, User user, Document document, Workflow workflow)
        {
            Id = id;
            User = user ?? throw new ArgumentNullException(nameof(user));
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
            Progress = new RequestProgress(id, workflow);
        }

        public bool IsApproved() => Progress.IsApproved;
        public bool IsRejected() => Progress.IsRejected;

        public void Approve(User user)
        {
            if (Progress.CurrentStep < Workflow.Steps.Count)
            {
                var currentStep = Workflow.Steps.ElementAt(Progress.CurrentStep);
                
                if (currentStep.IsInProgress(Progress))
                {
                    Progress.AdvanceStep(currentStep, user);
                    if (Progress.IsApproved)
                    {
                        _events.Add(new RequestApprovedEvent(Id));
                    }
                }
                else
                {
                    throw new InvalidOperationException("Cannot advance to the next step: either the request is already approved or rejected, or there are no more steps available.");
                }
            }
        }

        public void Reject(User user)
        {
            if (Progress is { IsRejected: false, IsApproved: false })
            {
                Progress.Reject(user);
                _events.Add(new RequestRejectEvent(Id));
            }
            else
            {
                throw new InvalidOperationException("Request is already approved or rejected");
            }
        }

        public void Restart()
        {
            Progress = new RequestProgress(Id, Workflow);
        }
    }
}
