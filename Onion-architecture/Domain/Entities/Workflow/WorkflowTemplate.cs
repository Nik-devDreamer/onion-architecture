using System;
using System.Collections.Generic;
using System.Linq;
using Onion_architecture.Domain.Entities.User;

namespace Onion_architecture.Domain.Entities.Workflow
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

        public Request.Request Create(User.User user, Document document)
        {
            // Создаем Workflow с нужными шагами
            List<WorkflowStep> steps = Steps.Select(t => new WorkflowStep(t.Name, t.Order, t.UserId, t.RoleId, "")).ToList();

            // Создаем новый экземпляр Workflow
            Workflow workflow = new Workflow(Id, Name, steps);

            // Создаем и возвращаем заявку
            return new Request.Request(Guid.NewGuid(), user.Id, document, workflow);
        }
    }
}