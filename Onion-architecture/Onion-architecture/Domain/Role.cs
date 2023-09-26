using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class Role
    {
        public Guid Id { get; private set; }
        public RoleType Type { get; private set; }

        public Role(RoleType type)
        {
            Id = Guid.NewGuid();
            Type = type;
        }
    }
}
