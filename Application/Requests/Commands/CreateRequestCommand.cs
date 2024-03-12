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
        UserId = userId != Guid.Empty ? userId : throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        Document = document ?? throw new ArgumentNullException(nameof(document), "Document cannot be null.");
        WorkflowTemplateId = workflowTemplateId != Guid.Empty ? workflowTemplateId : throw new ArgumentException("WorkflowTemplateId cannot be empty.", nameof(workflowTemplateId));
    }
}