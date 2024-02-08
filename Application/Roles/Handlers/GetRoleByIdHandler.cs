using Application.Factories;
using Application.Roles.Queries;
using onion_architecture.Domain.Entities.Users;

namespace Application.Roles.Handlers;

public class GetRoleByIdHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetRoleByIdHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Role GetRoleById(GetRoleByIdQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var roleRepository = tenant.Roles;

        return roleRepository.GetById(query.RoleId);
    }
}