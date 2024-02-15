using Application.Repositories;
using Application.Users.Commands;
using Domain.Entities.Users;

namespace Application.Users.Handlers;

public class CreateRoleHandler
{
    private readonly ITenantFactory _tenantFactory;

    public CreateRoleHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Guid Handle(CreateRoleCommand command)
    {
        var tenant = _tenantFactory.GetTenant();
        var roleRepository = tenant.Roles;

        var role = Role.Create(command.Name);

        roleRepository.Add(role);

        tenant.Commit();

        return role.Id;
    }
}