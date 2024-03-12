using Application.Repositories;
using Application.Requests.Queries;
using Domain.Entities.Requests;

namespace Application.Requests.Handlers;

public class GetAllRequestsHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetAllRequestsHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public IReadOnlyCollection<Request> Handle(GetAllRequestsQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var requestRepository = tenant.Requests;

        var allRequests = requestRepository.GetAll();

        return allRequests;
    }
}