using System;
using System.Collections.Generic;
using System.Linq;
using onion_architecture.Domain.Entities.Requests;
using onion_architecture.Domain.Entities.Users;

namespace onion_architecture.Domain.Entities.WorkflowTemplates
{
    public class WorkflowTemplate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<WorkflowStepTemplate> Steps { get; private set; }

        public WorkflowTemplate(Guid id, string name, List<WorkflowStepTemplate> steps)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }

        public Request Create(User user, Document document, string comment)
        {
            List<WorkflowStep> steps = Steps.Select
                (t => new WorkflowStep(t.Name, t.Order, t.UserId, t.RoleId, comment)).ToList();
            Workflow workflow = new Workflow(Id, Name, steps);
            return new Request(Guid.NewGuid(), user.Id, document, workflow);
        }
    }
}