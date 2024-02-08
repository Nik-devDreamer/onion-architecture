using Application.Repositories;

namespace Application.Services;

public class Tenant : ITenant
{
    public IUserRepository Users { get; }
    public IRoleRepository Roles { get; }
    public IWorkflowRepository Workflows { get; }
    public IRequestRepository Requests { get; }

    public Tenant(IUserRepository userRepository, IRoleRepository roleRepository, IWorkflowRepository workflowRepository, IRequestRepository requestRepository)
    {
        Users = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        Roles = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        Workflows = workflowRepository ?? throw new ArgumentNullException(nameof(workflowRepository));
        Requests = requestRepository ?? throw new ArgumentNullException(nameof(requestRepository));
    }
    
    public async Task CommitAsync()
    {
        
    }
}