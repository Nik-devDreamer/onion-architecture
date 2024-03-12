using System.Text.RegularExpressions;

namespace Domain.BaseObjectsNamespace
{
    public class Email
    {
        private static readonly Regex EmailRegex = new Regex(
            @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$", 
            RegexOptions.Compiled);

        public string Value { get; private set; }

        public Email(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
                throw new ArgumentException("Invalid email");
        }

        private bool IsValidEmail(string email)
        {
            return EmailRegex.IsMatch(email);
        }
    }
}
