using Application.Repositories;
using Application.Users.Queries;
using Domain.Entities.Users;

namespace Application.Users.Handlers;

public class GetUserByEmailHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetUserByEmailHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public User? Handle(GetUserByEmailQuery query)
    {
        return _tenantFactory.GetTenant().Users.TryGetByEmail(query.Email);
    }
}