using Application.Factories;
using onion_architecture.Domain.BaseObjectsNamespace;
using onion_architecture.Domain.Entities.Users;

namespace Application.Users;

public class CreateUserHandler
{
    private readonly ITenantFactory _tenantFactory;

    public CreateUserHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Guid CreateUser(CreateUserCommand command)
    {
        var tenant = _tenantFactory.GetTenant();
        var userRepository = tenant.Users;
        var roleRepository = tenant.Roles;

        var existingUser = userRepository.TryGetByEmail(command.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email is already in use.");
        }

        var role = roleRepository.GetById(command.RoleId);

        var password = new Password(command.Password);
        var user = User.Create(command.Name, new Email(command.Email), role, password);

        userRepository.Add(user);

        tenant.CommitAsync();

        return user.Id;
    }
}