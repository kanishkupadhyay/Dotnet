using System.Text.RegularExpressions;

namespace MyWebApi.Services.Validators;

public static class EmailValidator
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled
    );

    public static bool IsValid(string email)
    {
        return !string.IsNullOrWhiteSpace(email)
            && EmailRegex.IsMatch(email);
    }
}