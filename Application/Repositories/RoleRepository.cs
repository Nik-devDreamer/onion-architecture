using Application.Services;
using onion_architecture.Domain.Entities.Users;

namespace Application.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IRoleDataSource _roleDataSource;
    
    public RoleRepository(IRoleDataSource roleDataSource)
    {
        _roleDataSource = roleDataSource ?? throw new ArgumentNullException(nameof(roleDataSource));
    }
    
    public Role GetById(Guid roleId)
    {
        var role = _roleDataSource.GetRoleById(roleId);
        
        if (role == null)
        {
            throw new InvalidOperationException("Role not found.");
        }

        return role;
    }
}