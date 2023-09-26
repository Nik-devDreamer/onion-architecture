using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Onion_architecture.Domain
{
    public class Password
    {
        public string Value { get; private set; }

        public Password(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsValidPassword(value))
                throw new ArgumentException("Invalid password.");
        }

        private bool IsValidPassword(string password)
        {
            const string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@#$%^&+=]).*$";
            return Regex.IsMatch(password, pattern);
        }
    }
}
