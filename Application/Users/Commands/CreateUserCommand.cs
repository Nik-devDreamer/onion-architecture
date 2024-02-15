using Domain.BaseObjectsNamespace;

namespace Application.Users.Commands;

public class CreateUserCommand
{
    public string Name { get; set; }
    public Email Email { get; set; }
    public Guid RoleId { get; set; }
    public Password Password { get; set; }
}