namespace Application.Requests.Commands;

public class ApproveRequestCommand
{
    public Guid RequestId { get; }
    public Guid UserId { get; }

    public ApproveRequestCommand(Guid userId, Guid requestId)
    {
        UserId = userId != Guid.Empty ? userId : throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        RequestId = requestId != Guid.Empty ? requestId : throw new ArgumentException("RequestId cannot be empty.", nameof(requestId));
    }
}