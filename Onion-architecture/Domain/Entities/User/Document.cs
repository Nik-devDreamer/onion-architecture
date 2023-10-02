using System;

namespace Onion_architecture.Domain.Entities.User
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
