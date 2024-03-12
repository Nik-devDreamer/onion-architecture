using System.Text.RegularExpressions;

namespace Domain.BaseObjectsNamespace
{
    public class Password
    {
        private static readonly Regex PasswordRegex = new Regex(
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@#$%^&+=]).*$", 
            RegexOptions.Compiled);

        public string Value { get; private set; }

        public Password(string value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !IsValidPassword(value))
                throw new ArgumentException("Invalid password");
        }

        private bool IsValidPassword(string password)
        {
            return PasswordRegex.IsMatch(password);
        }
    }
}
