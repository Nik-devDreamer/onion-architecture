namespace Application.Requests.Commands;

public class RestartRequestCommand
{
    public Guid RequestId { get; }

    public RestartRequestCommand(Guid requestId)
    {
        RequestId = requestId != Guid.Empty ? requestId : throw new ArgumentException("RequestId cannot be empty.", nameof(requestId));
    }
}