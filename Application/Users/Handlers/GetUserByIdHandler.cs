using Application.Repositories;
using Application.Users.Queries;
using Domain.Entities.Users;

namespace Application.Users.Handlers;

public class GetUserByIdHandler
{
    private readonly ITenantFactory _tenantFactory;
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(ITenantFactory tenantFactory, IUserRepository userRepository)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public User? Handle(GetUserByIdQuery query)
    {
        return _userRepository.GetById(query.UserId);
    }
}