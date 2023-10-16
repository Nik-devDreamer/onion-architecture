using System;
using Onion_architecture.Domain.BaseObjectsNamespace;

namespace Onion_architecture.Domain.Entities.Requests
{
    public class Document
    {
        public Email Email { get; private set; }

        public Document(Email email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }
    }
}
