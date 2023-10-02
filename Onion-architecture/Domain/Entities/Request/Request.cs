using System;
using System.Collections.Generic;
using System.Linq;
using Onion_architecture.Domain.Entities.User;
using Onion_architecture.Domain.Entities.Workflow;


namespace Onion_architecture.Domain.Entities.Request
{
    public class Request
    {
        private List<IEvent.IEvent> Events;
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Document Document { get; private set; }
        public Workflow.Workflow Workflow { get; private set; }
        public RequestProgress Progress { get; private set; }
        public IReadOnlyList<IEvent.IEvent> EventsList => Events.AsReadOnly();

        public Request(Guid id, Guid userId, Document document, Workflow.Workflow workflow)
        {
            Id = id;
            UserId = userId;
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
            Events = new List<IEvent.IEvent>();
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

            if (currentStep != null)
            {
                Progress.AdvanceStep(currentStep);

                if (Progress.CurrentStep == Workflow.Steps.Count)
                {
                    Events.Add(new RequestApprovedEvent(Id));
                    Progress.Approve();
                }
            }
            else
            {
                throw new InvalidOperationException("No next step is available");
            }
        }

        public void Reject()
        {
            Events.Add(new RequestRejectEvent(Id));
            Progress.Reject();
        }

        public void Restart()
        {
            Events.RemoveAll(e => e is RequestApprovedEvent || e is RequestRejectEvent);
            Progress = new RequestProgress(Id);
        }
    }
}
