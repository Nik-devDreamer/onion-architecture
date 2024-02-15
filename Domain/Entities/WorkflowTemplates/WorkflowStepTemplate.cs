namespace Domain.Entities.WorkflowTemplates
{
    public class WorkflowStepTemplate
    {
        public string Name { get; private set; }
        public int Order { get; private set; }
        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }

        public WorkflowStepTemplate(string name, int order, Guid? userId, Guid? roleId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Order = order;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
