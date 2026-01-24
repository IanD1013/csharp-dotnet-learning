using System.Text.RegularExpressions;

namespace Demo.Domain;

public partial class PasswordValidator
{
    [GeneratedRegex("/(?=(.*[0-9]))(?=.*[\\!@#$%^&*()\\\\[\\]{}\\-_+=~`|:;\"'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}/ \n")]  
    private static partial Regex PasswordRegex();
    public static bool IsValid(string value) => PasswordRegex().IsMatch(value);
}