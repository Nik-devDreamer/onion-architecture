using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class WorkflowTemplate
    {
        public Guid Id { get; private set; }
        public RoleType Type { get; private set; }
        public List<WorkflowStepTemplate> Steps { get; private set; }

        public WorkflowTemplate(Guid id, RoleType type, List<WorkflowStepTemplate> steps)
        {
            Id = id;
            Type = type;
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }

        public Request Create(User user, Document document, EventStore eventStore)
        {
            // Создаем Workflow с нужными шагами
            List<WorkflowStep> steps = Steps.Select(t => new WorkflowStep(t.Name, t.Order, t.UserId, t.RoleId, "")).ToList();

            // Создаем новый экземпляр Workflow
            Workflow workflow = new Workflow(Id, Type, steps);

            // Создаем и возвращаем заявку
            return new Request(Guid.NewGuid(), user.Id, document, workflow, eventStore);
        }
    }
}