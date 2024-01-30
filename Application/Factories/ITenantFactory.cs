using Application.Repositories;

namespace Application.Factories;

public interface ITenantFactory
{
    ITenant GetTenant();
}