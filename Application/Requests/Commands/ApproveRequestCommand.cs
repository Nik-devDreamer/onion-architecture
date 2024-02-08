namespace Application.Requests.Commands;

public class ApproveRequestCommand
{
    public Guid RequestId { get; }

    public ApproveRequestCommand(Guid requestId)
    {
        RequestId = requestId;
    }
}