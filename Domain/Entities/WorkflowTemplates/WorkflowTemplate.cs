using Domain.Entities.Requests;
using Domain.Entities.Users;

namespace Domain.Entities.WorkflowTemplates
{
    public class WorkflowTemplate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<WorkflowStepTemplate> Steps { get; private set; }

        public WorkflowTemplate(Guid id, string name, WorkflowStepTemplate[] steps)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }
        
        public static WorkflowTemplate Create(string name, WorkflowStepTemplate[] steps)
        {
            return new WorkflowTemplate(Guid.NewGuid(), name, steps);
        }
        
        public Request CreateRequest(User user, Document document)
        {
            Workflow workflow = Workflow.Create(this);
            return new Request(Guid.NewGuid(), user.Id, document, workflow);
        }
    }
}