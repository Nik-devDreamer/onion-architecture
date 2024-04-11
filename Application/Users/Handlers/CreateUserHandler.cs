using Application.Repositories;
using Application.Users.Commands;
using Domain.BaseObjectsNamespace;
using Domain.Entities.Users;

namespace Application.Users.Handlers;

public class CreateUserHandler
{
    private readonly ITenantFactory _tenantFactory;

    public CreateUserHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Guid Handle(CreateUserCommand command)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(command));
        }
        
        var tenant = _tenantFactory.GetTenant();
        var userRepository = tenant.Users;
        var roleRepository = tenant.Roles;

        var existingUser = userRepository.TryGetByEmail(command.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email is already in use.");
        }

        var role = roleRepository.GetById(command.RoleId);

        var password = command.Password;
        var user = User.Create(command.Name, command.Email, role, password);

        userRepository.Add(user);

        tenant.Commit();

        return user.Id;
    }
}