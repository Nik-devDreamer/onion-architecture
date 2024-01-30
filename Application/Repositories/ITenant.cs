namespace Application.Repositories;

public interface ITenant
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IWorkflowRepository Workflows { get; }
    IRequestRepository Requests { get; }

    Task CommitAsync();
}