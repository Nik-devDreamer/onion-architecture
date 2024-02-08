namespace Application.Users.Queries;

public class GetUserByEmailQuery
{
    public string Email { get; }

    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }
}