namespace Application.WorkflowTemplates.Queries;

public class GetWorkflowTemplateByIdQuery
{
    public Guid WorkflowTemplateId { get; }

    public GetWorkflowTemplateByIdQuery(Guid workflowTemplateId)
    {
        WorkflowTemplateId = workflowTemplateId != Guid.Empty ? workflowTemplateId : throw new ArgumentException("WorkflowTemplateId cannot be empty.", nameof(workflowTemplateId));
    }
}