using onion_architecture.Domain.Entities.Requests;

namespace Application.Repositories;

public interface IRequestRepository
{
    Request GetById(Guid requestId);
    
    void Add(Request request);
}