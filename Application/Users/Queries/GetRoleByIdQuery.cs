namespace Application.Users.Queries;

public class GetRoleByIdQuery
{
    public Guid RoleId { get; }

    public GetRoleByIdQuery(Guid roleId)
    {
        RoleId = roleId != Guid.Empty ? roleId : throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));
    }
}