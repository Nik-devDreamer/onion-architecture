using Application.Repositories;
using Application.Requests.Commands;
using Domain.Entities.Requests;

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

        var workflowTemplate = command.WorkflowTemplate;
        var steps = workflowTemplate.Steps.Select(stepTemplate => new WorkflowStep(stepTemplate.Name, stepTemplate.Order, null, null, null)).ToArray();
        var workflow = new Workflow(Guid.NewGuid(), workflowTemplate.Name, steps);

        var request = new Request(Guid.NewGuid(), command.UserId, command.Document, workflow);

        requestRepository.Add(request);

        tenant.Commit();
    }
}