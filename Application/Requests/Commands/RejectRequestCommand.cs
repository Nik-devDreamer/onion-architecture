namespace Application.Requests.Commands;

public class RejectRequestCommand
{
    public Guid RequestId { get; }

    public RejectRequestCommand(Guid requestId)
    {
        RequestId = requestId != Guid.Empty ? requestId : throw new ArgumentException("RequestId cannot be empty.", nameof(requestId));
    }
}