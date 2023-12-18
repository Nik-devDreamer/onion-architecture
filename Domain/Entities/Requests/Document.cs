using System;
using onion_architecture.Domain.BaseObjectsNamespace;

namespace onion_architecture.Domain.Entities.Requests
{
    public class Document
    {
        public Email Email { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime DateOfBirth { get; private set; }

        public Document(Email email, string name, string phoneNumber, DateTime dateOfBirth)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            DateOfBirth = dateOfBirth;
        }
    }
}
