using onion_architecture.Domain.Entities.Users;

namespace Application.Repositories;

public interface IUserRepository
{
    User? TryGetByEmail(string email);
    
    void Add(User user);
}