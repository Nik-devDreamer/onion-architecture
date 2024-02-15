namespace Application.Users.Queries;

public class GetRoleByIdQuery
{
    public Guid RoleId { get; }

    public GetRoleByIdQuery(Guid roleId)
    {
        RoleId = roleId;
    }
}