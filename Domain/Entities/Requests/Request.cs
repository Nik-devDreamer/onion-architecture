using Domain.BaseObjectsNamespace;
using Domain.Entities.Requests.Events;

namespace Domain.Entities.Requests
{
    public class Request
    {
        private List<IEvent> _events = new List<IEvent>();

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Document Document { get; private set; }
        public Workflow Workflow { get; private set; }
        public RequestProgress Progress { get; private set; }
        public IReadOnlyList<IEvent> EventsList => _events.AsReadOnly();

        public Request(Guid id, Guid userId, Document document, Workflow workflow)
        {
            Id = id;
            UserId = userId;
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
            Progress = new RequestProgress(id, workflow);
        }

        public bool IsApproved()
        {
            return Progress.IsApproved;
        }

        public bool IsRejected()
        {
            return Progress.IsRejected;
        }

        public void Approve()
        {
            if (Progress.CurrentStep < Workflow.Steps.Count && Progress is { IsApproved: false, IsRejected: false }) 
            {
                Progress.AdvanceStep(Workflow.Steps[Progress.CurrentStep], UserId);

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

        public void Reject()
        {
            if (Progress is { IsRejected: false, IsApproved: false })
            {
                Progress.Reject(UserId);
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
