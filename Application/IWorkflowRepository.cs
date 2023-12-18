using onion_architecture.Domain.Entities.WorkflowTemplates;

namespace Application;

public interface IWorkflowRepository
{
    WorkflowTemplate GetWorkflowTemplateById(Guid workflowTemplateId);
}