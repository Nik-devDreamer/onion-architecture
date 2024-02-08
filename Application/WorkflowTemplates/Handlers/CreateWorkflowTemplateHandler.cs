using Application.Factories;
using Application.Workflows.Commands;
using onion_architecture.Domain.Entities.WorkflowTemplates;

namespace Application.Workflows.Handlers;

public class CreateWorkflowTemplateHandler
{
    private readonly ITenantFactory _tenantFactory;

    public CreateWorkflowTemplateHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Guid CreateWorkflowTemplate(CreateWorkflowTemplateCommand command)
    {
        var tenant = _tenantFactory.GetTenant();
        var workflowRepository = tenant.Workflows;

        var workflowTemplate = WorkflowTemplate.Create(command.Name, command.Steps);

        workflowRepository.Add(workflowTemplate);

        tenant.CommitAsync();

        return workflowTemplate.Id;
    }
}