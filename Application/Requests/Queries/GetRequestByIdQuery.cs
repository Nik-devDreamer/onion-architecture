namespace Application.Requests.Queries;

public class GetRequestByIdQuery
{
    public Guid RequestId { get; }

    public GetRequestByIdQuery(Guid requestId)
    {
        RequestId = requestId;
    }
}