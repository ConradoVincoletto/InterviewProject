using System.Text.RegularExpressions;

namespace Domain.Validation;

public static class EmailValidation
{
    public static bool ValidateEmail(string email)
    {
        // Regular expression to validate the email format
        const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }
}
