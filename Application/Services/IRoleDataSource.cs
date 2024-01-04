using onion_architecture.Domain.Entities.Users;

namespace Application.Services;

public interface IRoleDataSource
{
    Role GetRoleById(Guid roleId);
}