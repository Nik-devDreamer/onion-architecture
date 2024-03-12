using Application.Requests.Commands;
using Domain.Entities.Requests;

namespace Application.Repositories;

public interface IRequestRepository
{
    Request GetById(Guid requestId);
    void Add(Request request);
    void Create(CreateRequestCommand command);
    IReadOnlyCollection<Request> GetAll();
}