using System;
using System.Text.RegularExpressions;

namespace Onion_architecture.Domain.Entities.User
{
    public class Email
    {
        public string Value { get; private set; }

        public Email(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
                throw new ArgumentException("Invalid email.");
        }

        private bool IsValidEmail(string email)
        {
            const string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
