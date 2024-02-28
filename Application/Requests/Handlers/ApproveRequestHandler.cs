using Application.Repositories;
using Application.Requests.Commands;

namespace Application.Requests.Handlers;

public class ApproveRequestHandler
{
    private readonly ITenantFactory _tenantFactory;

    public ApproveRequestHandler(ITenantFactory tenantFactory, IUserRepository userRepository)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }
    
    public void Handle(ApproveRequestCommand command)
    {
        var tenant = _tenantFactory.GetTenant();
        var requestRepository = tenant.Requests;

        var request = requestRepository.GetById(command.RequestId);
        request.Approve(request.UserId);

        tenant.Commit();
    }
}