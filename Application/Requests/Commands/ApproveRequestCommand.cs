namespace Application.Requests.Commands;

public class ApproveRequestCommand
{
    public Guid RequestId { get; }

    public ApproveRequestCommand(Guid requestId)
    {
        RequestId = requestId != Guid.Empty ? requestId : throw new ArgumentException("RequestId cannot be empty.", nameof(requestId));
    }
}