using Domain.Entities.Users;

namespace Application.Repositories;

public interface IRoleRepository
{
    Role GetById(Guid roleId);
    void Add(Role role);
}