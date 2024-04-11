using Application.Repositories;
using Application.WorkflowTemplates.Commands;
using Domain.Entities.WorkflowTemplates;

namespace Application.WorkflowTemplates.Handlers;

public class CreateWorkflowTemplateHandler
{
    private readonly ITenantFactory _tenantFactory;

    public CreateWorkflowTemplateHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Guid Handle(CreateWorkflowTemplateCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }
        
        var tenant = _tenantFactory.GetTenant();
        var workflowRepository = tenant.WorkflowsTemplate;

        var workflowTemplate = WorkflowTemplate.Create(command.Name, command.Steps);

        workflowRepository.Add(workflowTemplate);

        tenant.Commit();

        return workflowTemplate.Id;
    }
}