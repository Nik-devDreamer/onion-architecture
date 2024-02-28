using Domain.Entities.Requests;
using Domain.Entities.WorkflowTemplates;

namespace Application.Requests.Commands;

public class CreateRequestCommand
{
    public Guid UserId { get; }
    public Document Document { get; }
    public Guid WorkflowTemplateId { get; }
    
    public CreateRequestCommand(Guid userId, Document document, Guid workflowTemplateId)
    {
        UserId = userId;
        Document = document;
        WorkflowTemplateId = workflowTemplateId;
    }
}