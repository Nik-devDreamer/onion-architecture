using Domain.BaseObjectsNamespace;

namespace Application.Users.Commands;

public class CreateUserCommand
{
    public string Name { get; set; }
    public Email Email { get; set; }
    public Guid RoleId { get; set; }
    public Password Password { get; set; }
    
    public CreateUserCommand(string name, Email email, Guid roleId, Password password)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        RoleId = roleId != Guid.Empty ? roleId : throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}