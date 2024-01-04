namespace Application.Models;

public class CreateUserCommand
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid RoleId { get; set; }
    public string Password { get; set; }
}