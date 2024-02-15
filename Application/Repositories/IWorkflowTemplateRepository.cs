using Domain.Entities.WorkflowTemplates;

namespace Application.Repositories;

public interface IWorkflowTemplateRepository
{
    WorkflowTemplate GetWorkflowTemplateById(Guid workflowTemplateId);
    void Add(WorkflowTemplate workflowTemplate);
}