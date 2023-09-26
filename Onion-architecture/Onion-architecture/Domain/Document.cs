using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class Document
    {
        public Email Email { get; private set; }

        public Document(Email email)
        {
            Email = email;
        }
    }
}
