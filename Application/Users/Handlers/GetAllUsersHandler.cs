using Application.Repositories;
using Application.Users.Queries;
using Domain.Entities.Users;

namespace Application.Users.Handlers;

public class GetAllUsersHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetAllUsersHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public IReadOnlyCollection<User> Handle(GetAllUsersQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var userRepository = tenant.Users;

        var allUsers = userRepository.GetAll();

        return allUsers;
    }
}