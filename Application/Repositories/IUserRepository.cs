using Domain.BaseObjectsNamespace;
using Domain.Entities.Users;

namespace Application.Repositories;

public interface IUserRepository
{
    User? TryGetByEmail(Email email);
    void Add(User user);
}