using System;

namespace Onion_architecture.Domain.Entities.User
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Guid RoleId { get; private set; }
        public Password Password { get; private set; }

        public User(Guid id, string name, Email email, Guid roleId, Password password)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            RoleId = roleId;
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public static User Create(string name, string email, string roleName, string password)
        {
            Email userEmail = new Email(email);
            Role role = new Role(roleName);
            Password userPassword = new Password(password);
            return new User(Guid.NewGuid(), name, userEmail, role.Id, userPassword);
        }
    }
}
