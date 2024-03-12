using Application.Repositories;
using Application.WorkflowTemplates.Queries;
using Domain.Entities.WorkflowTemplates;

namespace Application.WorkflowTemplates.Handlers;

public class GetAllWorkflowTemplatesHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetAllWorkflowTemplatesHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public IReadOnlyCollection<WorkflowTemplate> Handle(GetAllWorkflowTemplatesQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var workflowRepository = tenant.WorkflowsTemplate;

        var allWorkflowTemplates = workflowRepository.GetAll();

        return allWorkflowTemplates;
    }
}