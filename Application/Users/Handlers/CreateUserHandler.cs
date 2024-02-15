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
        var tenant = _tenantFactory.GetTenant();
        var userRepository = tenant.Users;
        var roleRepository = tenant.Roles;

        var existingUser = userRepository.TryGetByEmail(command.Email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Email is already in use.");
        }

        var role = roleRepository.GetById(command.RoleId);

        var password = new Password(command.Password.ToString() ?? throw new InvalidOperationException());
        var user = User.Create(command.Name, new Email(command.Email.ToString() ?? throw new InvalidOperationException()), role, password);

        userRepository.Add(user);

        tenant.Commit();

        return user.Id;
    }
}