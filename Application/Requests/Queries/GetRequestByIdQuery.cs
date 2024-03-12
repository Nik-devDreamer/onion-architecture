namespace Application.Requests.Queries;

public class GetRequestByIdQuery
{
    public Guid RequestId { get; }

    public GetRequestByIdQuery(Guid requestId)
    {
        RequestId = requestId != Guid.Empty ? requestId : throw new ArgumentException("RequestId cannot be empty.", nameof(requestId));
    }
}