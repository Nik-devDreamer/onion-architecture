using Domain.Entities.WorkflowTemplates;

namespace Application.Repositories;

public interface IWorkflowTemplateRepository
{
    WorkflowTemplate GetById(Guid workflowTemplateId);
    void Add(WorkflowTemplate workflowTemplate);
}