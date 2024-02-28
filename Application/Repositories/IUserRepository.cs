using Domain.BaseObjectsNamespace;
using Domain.Entities.Users;

namespace Application.Repositories;

public interface IUserRepository
{
    User TryGetByEmail(Email email);
    User GetById(Guid userId);
    void Add(User user);
}