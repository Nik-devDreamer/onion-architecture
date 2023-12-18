using onion_architecture.Domain.Entities.Users;

namespace Application;

public interface IRoleRepository
{
    Role GetById(Guid roleId);
}