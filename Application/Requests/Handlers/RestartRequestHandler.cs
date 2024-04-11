using Application.Repositories;
using Application.Requests.Commands;

namespace Application.Requests.Handlers;

public class RestartRequestHandler
{
    private readonly ITenantFactory _tenantFactory;

    public RestartRequestHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public void Handle(RestartRequestCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }
        
        var tenant = _tenantFactory.GetTenant();
        var requestRepository = tenant.Requests;

        var request = requestRepository.GetById(command.RequestId);
        request.Restart();

        tenant.Commit();
    }
}