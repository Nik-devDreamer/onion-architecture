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
        var userRepository = tenant.Users;

        var request = requestRepository.GetById(command.RequestId);
        var user = userRepository.GetById(request.User.Id);
        request.Reject(user);

        tenant.Commit();
    }
}