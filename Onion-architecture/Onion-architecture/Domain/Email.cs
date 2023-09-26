using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
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
            string emailPattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
