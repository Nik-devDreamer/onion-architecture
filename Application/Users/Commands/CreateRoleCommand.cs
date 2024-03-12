namespace Application.Users.Commands;

public class CreateRoleCommand
{
    public string Name { get; set; }
    
    public CreateRoleCommand(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}