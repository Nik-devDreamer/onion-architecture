using System;

namespace Onion_architecture.Domain.Entities.Workflow
{
    public class WorkflowStep
    {
        public string Name { get; private set; }
        public int Order { get; private set; }
        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }
        public string Comment { get; private set; }

        public WorkflowStep(string name, int order, Guid userId, Guid roleId, string comment)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Order = order;
            UserId = userId;
            RoleId = roleId;
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
        }

        public void UpdateComment(string comment)
        {
            Comment = comment ?? throw new ArgumentNullException(nameof(comment));
        }
    }
}
