namespace Application.Repositories;

public interface ITenant
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IWorkflowTemplateRepository WorkflowsTemplate { get; }
    IRequestRepository Requests { get; }

    void Commit();
}