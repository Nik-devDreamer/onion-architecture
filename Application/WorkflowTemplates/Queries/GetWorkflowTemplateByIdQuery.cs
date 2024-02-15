namespace Application.WorkflowTemplates.Queries;

public class GetWorkflowTemplateByIdQuery
{
    public Guid WorkflowTemplateId { get; }

    public GetWorkflowTemplateByIdQuery(Guid workflowTemplateId)
    {
        WorkflowTemplateId = workflowTemplateId;
    }
}