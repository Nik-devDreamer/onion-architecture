using Application.Factories;
using Application.Repositories;

namespace Application.Services;

public class TenantFactory : ITenantFactory
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IWorkflowRepository _workflowRepository;
    private readonly IRequestRepository _requestRepository;

    public TenantFactory(IUserRepository userRepository, IRoleRepository roleRepository, IWorkflowRepository workflowRepository, IRequestRepository requestRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        _workflowRepository = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
        _requestRepository = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
    }

    public ITenant GetTenant()
    {
        return new Tenant(_userRepository, _roleRepository, _workflowRepository, _requestRepository);
    }
}