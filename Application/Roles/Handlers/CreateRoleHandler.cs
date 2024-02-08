using Application.Factories;
using Application.Roles.Commands;
using onion_architecture.Domain.Entities.Users;

namespace Application.Roles.Handlers;

public class CreateRoleHandler
{
    private readonly ITenantFactory _tenantFactory;

    public CreateRoleHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Guid CreateRole(CreateRoleCommand command)
    {
        var tenant = _tenantFactory.GetTenant();
        var roleRepository = tenant.Roles;

        var role = Role.Create(command.Name);

        roleRepository.Add(role);

        tenant.CommitAsync();

        return role.Id;
    }
}