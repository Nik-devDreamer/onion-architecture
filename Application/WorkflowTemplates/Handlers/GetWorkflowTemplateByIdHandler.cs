using Application.Repositories;
using Application.WorkflowTemplates.Queries;
using Domain.Entities.WorkflowTemplates;

namespace Application.WorkflowTemplates.Handlers;

public class GetWorkflowTemplateByIdHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetWorkflowTemplateByIdHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public WorkflowTemplate Handle(GetWorkflowTemplateByIdQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var workflowRepository = tenant.WorkflowsTemplate;

        return workflowRepository.GetWorkflowTemplateById(query.WorkflowTemplateId);
    }
}