using Application.Repositories;
using Application.Requests.Commands;

namespace Application.Requests.Handlers;

public class ApproveRequestHandler
{
    private readonly ITenantFactory _tenantFactory;

    public ApproveRequestHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }
    
    public void Handle(ApproveRequestCommand command)
    {
        var tenant = _tenantFactory.GetTenant();
        var requestRepository = tenant.Requests;
        var userRepository = tenant.Users;

        var request = requestRepository.GetById(command.RequestId);
        var user = userRepository.GetById(request.User.Id);
        request.Approve(user);

        tenant.Commit();
    }
}