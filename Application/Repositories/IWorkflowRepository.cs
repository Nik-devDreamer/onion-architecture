using onion_architecture.Domain.Entities.WorkflowTemplates;

namespace Application.Repositories;

public interface IWorkflowRepository
{
    WorkflowTemplate GetWorkflowTemplateById(Guid workflowTemplateId);
    void Add(WorkflowTemplate workflowTemplate);
}