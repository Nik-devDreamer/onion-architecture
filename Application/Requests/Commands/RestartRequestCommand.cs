namespace Application.Requests.Commands;

public class RestartRequestCommand
{
    public Guid RequestId { get; }

    public RestartRequestCommand(Guid requestId)
    {
        RequestId = requestId;
    }
}