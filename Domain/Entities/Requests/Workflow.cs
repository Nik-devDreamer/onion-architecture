namespace Domain.Entities.Requests
{
    public class Workflow
    {
        public Guid WorkflowTemplateId { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyList<WorkflowStep> Steps { get; private set; }

        public Workflow(Guid workflowTemplateId, string name, WorkflowStep[] steps)
        {
            WorkflowTemplateId = workflowTemplateId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Steps = steps ?? throw new ArgumentNullException(nameof(steps));
        }
        
        public static Workflow Create(string name, WorkflowStep[] steps)
        {
            return new Workflow(Guid.NewGuid(), name, steps);
        }
    }
}
