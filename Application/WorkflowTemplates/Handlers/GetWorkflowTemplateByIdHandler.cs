using Application.Factories;
using Application.Workflows.Queries;
using onion_architecture.Domain.Entities.WorkflowTemplates;

namespace Application.Workflows.Handlers;

public class GetWorkflowTemplateByIdHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetWorkflowTemplateByIdHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public WorkflowTemplate GetWorkflowTemplateById(GetWorkflowTemplateByIdQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var workflowRepository = tenant.Workflows;

        return workflowRepository.GetWorkflowTemplateById(query.WorkflowTemplateId);
    }
}