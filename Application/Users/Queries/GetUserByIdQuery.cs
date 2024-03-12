namespace Application.Users.Queries;

public class GetUserByIdQuery
{
    public Guid UserId { get; }

    public GetUserByIdQuery(Guid userId)
    {
        UserId = userId != Guid.Empty ? userId : throw new ArgumentException("UserId cannot be empty.", nameof(userId));
    }
}