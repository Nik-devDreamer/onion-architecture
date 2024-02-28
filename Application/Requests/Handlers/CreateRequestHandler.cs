using Application.Repositories;
using Application.Requests.Commands;
using Domain.Entities.Requests;
using Domain.Entities.WorkflowTemplates;

namespace Application.Requests.Handlers;

public class CreateRequestHandler
{
    private readonly ITenantFactory _tenantFactory;

    public CreateRequestHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public void Handle(CreateRequestCommand command)
    {
        var tenant = _tenantFactory.GetTenant();
        var requestRepository = tenant.Requests;

        var workflowTemplateId = command.WorkflowTemplateId;
        var workflowTemplate = tenant.WorkflowsTemplate.GetById(workflowTemplateId);
        var workflow = Workflow.Create(workflowTemplate);

        var request = new Request(Guid.NewGuid(), command.UserId, command.Document, workflow);

        requestRepository.Add(request);

        tenant.Commit();
    }
}