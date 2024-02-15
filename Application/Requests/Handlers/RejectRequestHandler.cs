using Application.Repositories;
using Application.Requests.Commands;

namespace Application.Requests.Handlers;

public class RejectRequestHandler
{
    private readonly ITenantFactory _tenantFactory;

    public RejectRequestHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public void Handle(RejectRequestCommand command)
    {
        var tenant = _tenantFactory.GetTenant();
        var requestRepository = tenant.Requests;

        var request = requestRepository.GetById(command.RequestId);
        request.Reject();

        tenant.Commit();
    }
}