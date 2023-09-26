using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class Request
    {
        private List<IEvent> Events;
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Document Document { get; private set; }
        public Workflow Workflow { get; private set; }
        public RequestProgress Progress { get; private set; }
        public EventStore EventStore { get; private set; }

        public Request(Guid id, Guid userId, Document document, Workflow workflow, EventStore eventStore)
        {
            Id = id;
            UserId = userId;
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Workflow = workflow ?? throw new ArgumentNullException(nameof(workflow));
            Events = new List<IEvent>();
            Progress = new RequestProgress(id);
            EventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        public bool IsApproved()
        {
            // для определения, одобрена ли заявка
            return Progress.IsApproved;
        }

        public bool IsRejected()
        {
            // для определения, отклонена ли заявка
            return Events.Exists(e => e is RequestRejectEvent);
        }

        public void Approve(User user, int stepOrder)
        {
            // для одобрения заявки
            WorkflowStep targetStep = Workflow.Steps.FirstOrDefault(s => s.Order == stepOrder);
            if (targetStep == null)
                throw new InvalidOperationException("Incorrect step order");

            targetStep.UpdateComment("Approved");
            Progress.AdvanceStep();

            if (Progress.CurrentStep == Workflow.Steps.Count)
            {
                Progress.Approve();
                Events.Add(new RequestApprovedEvent(Id));
                EventStore.Add(new RequestApprovedEvent(Id));
            }
        }

        public void Reject(User user)
        {
            // для отклонения заявки
            Events.Add(new RequestRejectEvent(Id));
        }

        public void Restart()
        {
            // для перезапуска заявки
            Events.RemoveAll(e => e is RequestApprovedEvent || e is RequestRejectEvent);
            Progress = new RequestProgress(Id);
            EventStore.RemoveEventsByRequestId(Id);
        }
    }
}
