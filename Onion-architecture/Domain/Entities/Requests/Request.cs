using System;
using System.Collections.Generic;
using System.Linq;
using Onion_architecture.Domain.Entities.Workflows;
using Onion_architecture.Domain.BaseObjectsNamespace;


namespace Onion_architecture.Domain.Entities.Requests
{
    public class Request
    {
        private List<IEvent> _events = new List<IEvent>();

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Document Document { get; private set; }
        public Workflow Workflow { get; private set; }
        public RequestProgress Progress { get; private set; }
        public IReadOnlyList<IEvent> eventsList => _events.AsReadOnly();

        public Request(Guid id, Guid userId, Document document, Workflow workflow)
        {
            Id = id;
            UserId = userId;
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
            Progress = new RequestProgress(id);
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
            WorkflowStep currentStep = Workflow.Steps
                .OrderBy(step => step.Order)
                .SingleOrDefault(step => step.Order == Progress.CurrentStep + 1);

            IEvent ev = Progress.AdvanceStep(currentStep, Workflow.Steps.Count);
            if (ev != null)
            {
                _events.Add(ev);
            }
        }

        public void Reject()
        {
            _events.Add(new RequestRejectEvent(Id));
            Progress.Reject();
        }

        public void Restart()
        {
            Progress = new RequestProgress(Id);
        }
    }
}
