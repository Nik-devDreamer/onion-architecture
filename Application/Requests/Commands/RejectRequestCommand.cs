namespace Application.Requests.Commands;

public class RejectRequestCommand
{
    public Guid RequestId { get; }

    public RejectRequestCommand(Guid requestId)
    {
        RequestId = requestId;
    }
}