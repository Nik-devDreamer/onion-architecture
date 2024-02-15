using Domain.BaseObjectsNamespace;

namespace Application.Users.Queries;

public class GetUserByEmailQuery
{
    public Email Email { get; }

    public GetUserByEmailQuery(Email email)
    {
        Email = email;
    }
}