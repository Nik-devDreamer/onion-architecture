using Domain.Entities.WorkflowTemplates;

namespace Application.WorkflowTemplates.Commands;

public class CreateWorkflowTemplateCommand
{
    public string Name { get; set; }
    public WorkflowStepTemplate[] Steps { get; set; }
    
    public CreateWorkflowTemplateCommand(string name, WorkflowStepTemplate[] steps)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Steps = steps ?? throw new ArgumentNullException(nameof(steps));
    }
}