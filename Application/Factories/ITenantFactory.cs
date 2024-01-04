using Application.Services;

namespace Application.Factories;

public interface ITenantFactory
{
    ITenant GetTenant();
}