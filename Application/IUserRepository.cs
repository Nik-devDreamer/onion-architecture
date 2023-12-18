using onion_architecture.Domain.Entities.Users;

namespace Application;

public interface IUserRepository
{
    User TryGetByEmail(string email);
    
    void Add(User user);
}