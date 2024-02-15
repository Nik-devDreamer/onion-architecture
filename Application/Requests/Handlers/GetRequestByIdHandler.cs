using Application.Repositories;
using Application.Requests.Queries;
using Domain.Entities.Requests;

namespace Application.Requests.Handlers;

public class GetRequestByIdHandler
{
    private readonly ITenantFactory _tenantFactory;

    public GetRequestByIdHandler(ITenantFactory tenantFactory)
    {
        _tenantFactory = tenantFactory ?? throw new ArgumentNullException(nameof(tenantFactory));
    }

    public Request Handle(GetRequestByIdQuery query)
    {
        var tenant = _tenantFactory.GetTenant();
        var requestRepository = tenant.Requests;

        return requestRepository.GetById(query.RequestId);
    }
}