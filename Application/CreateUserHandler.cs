using onion_architecture.Domain.BaseObjectsNamespace;
using onion_architecture.Domain.Entities.Users;

namespace Application;

public class CreateUserHandler
{
    private readonly ITenant _tenant;

    public CreateUserHandler(ITenant tenant)
    {
        _tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
    }

    public Guid CreateUser(CreateUserCommand command)
    {
        var userRepository = _tenant.Users;
        var roleRepository = _tenant.Roles;

        var existingUser = userRepository.TryGetByEmail(command.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email is already in use.");
        }

        var role = roleRepository.GetById(command.RoleId);
        if (role == null)
        {
            throw new InvalidOperationException("Role not found.");
        }

        var password = new Password(command.Password);
        var user = User.Create(command.Name, new Email(command.Email), role, password);

        userRepository.Add(user);

        _tenant.Commit();

        return user.Id;
    }
}