using Application.Repositories;
using Application.Users.Queries;
using Domain.Entities.Users;

namespace Application.Users.Handlers;

public class GetRoleByIdHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetRoleByIdHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Role Handle(GetRoleByIdQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var roleRepository = tenant.Roles;

        return roleRepository.GetById(query.RoleId);
    }
}