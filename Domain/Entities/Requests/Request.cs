using System;
using System.Collections.Generic;
using System.Linq;
using onion_architecture.Domain.Entities.Requests.Events;
using onion_architecture.Domain.BaseObjectsNamespace;


namespace onion_architecture.Domain.Entities.Requests
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
            if (Progress.CurrentStep < Workflow.Steps.Count && !Progress.IsApproved)
            {
                Progress.AdvanceStep(Workflow.Steps[Progress.CurrentStep], UserId);

                if (Progress.IsApproved)
                {
                    _events.Add(new RequestApprovedEvent(Id));
                }
            }
            else
            {
                throw new InvalidOperationException("No next step is available");
            }
        }

        public void Reject()
        {
            if (!Progress.IsRejected)
            {
                _events.Add(new RequestRejectEvent(Id));
                Progress.Reject(UserId);
            }
        }

        public void Restart()
        {
            Progress = new RequestProgress(Id, Workflow);
        }
    }
}
