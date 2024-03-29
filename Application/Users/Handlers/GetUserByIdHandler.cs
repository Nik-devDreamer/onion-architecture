using Application.Repositories;
using Application.Users.Queries;
using Domain.Entities.Users;

namespace Application.Users.Handlers;

public class GetUserByIdHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetUserByIdHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public User? Handle(GetUserByIdQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var userRepository = tenant.Users;
        
        return userRepository.GetById(query.UserId);
    }
}