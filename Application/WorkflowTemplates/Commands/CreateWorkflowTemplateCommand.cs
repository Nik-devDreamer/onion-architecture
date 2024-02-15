using Domain.Entities.WorkflowTemplates;

namespace Application.WorkflowTemplates.Commands;

public class CreateWorkflowTemplateCommand
{
    public string Name { get; set; }
    public WorkflowStepTemplate[] Steps { get; set; }
}