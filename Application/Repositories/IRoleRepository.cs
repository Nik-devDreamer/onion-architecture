using onion_architecture.Domain.Entities.Users;

namespace Application.Repositories;

public interface IRoleRepository
{
    Role GetById(Guid roleId);
    void Add(Role role);
}