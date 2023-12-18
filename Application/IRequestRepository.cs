using onion_architecture.Domain.Entities.Requests;

namespace Application;

public interface IRequestRepository
{
    Request GetById(Guid requestId);
    
    void Add(Request request);
}