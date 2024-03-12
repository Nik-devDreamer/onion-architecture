using Domain.Entities.WorkflowTemplates;

namespace Domain.Entities.Requests
{
    public class WorkflowStep
    {
        public string Name { get; private set; }
        public int Order { get; private set; }
        public Guid? UserId { get; private set; }
        public Guid? RoleId { get; private set; }
        public string? Comment { get; private set; }

        public WorkflowStep(string name, int order, Guid? userId, Guid? roleId, string? comment)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Order = order;
            UserId = userId;
            RoleId = roleId;
            Comment = comment;
        }

        public static WorkflowStep Create(string name, int order, Guid? userId, Guid? roleId, string? comment)
        {
            return new WorkflowStep(name, order, userId, roleId, comment);
        }
        
        public static WorkflowStep Create(WorkflowStepTemplate template)
        {
            return new WorkflowStep(template.Name, template.Order, template.UserId, template.RoleId, null);
        }

        public void UpdateComment(string? comment)
        {
            Comment = comment;
        }
        
        public bool IsInProgress(RequestProgress progress)
        {
            return progress.CurrentStep < Order && progress is { IsApproved: false, IsRejected: false };
        }
    }
}
