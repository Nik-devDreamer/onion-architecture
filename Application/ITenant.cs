namespace Application;

public interface ITenant
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IWorkflowRepository Workflows { get; }
    IRequestRepository Requests { get; }

    void Commit();
    Task CommitAsync();
}