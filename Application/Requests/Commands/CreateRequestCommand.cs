using Domain.Entities.Requests;
using Domain.Entities.WorkflowTemplates;

namespace Application.Requests.Commands;

public class CreateRequestCommand
{
    public Guid UserId { get; }
    public Document Document { get; }
    public WorkflowTemplate WorkflowTemplate { get; }
    
    public CreateRequestCommand(Guid userId, Document document, WorkflowTemplate workflowTemplate)
    {
        UserId = userId;
        Document = document;
        WorkflowTemplate = workflowTemplate;
    }
}