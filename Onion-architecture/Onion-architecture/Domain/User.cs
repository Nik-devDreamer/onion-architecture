using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; }
        public Password Password { get; private set; }

        public User(Guid id, string name, Email email, Guid roleId, Role role, Password password)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            RoleId = roleId;
            Role = role ?? throw new ArgumentNullException(nameof(role));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }
}
