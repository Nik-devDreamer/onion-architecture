using onion_architecture.Domain.Entities.WorkflowTemplates;

namespace Application.Workflows.Commands;

public class CreateWorkflowTemplateCommand
{
    public string Name { get; set; }
    public WorkflowStepTemplate[] Steps { get; set; }
}