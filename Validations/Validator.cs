using System.Text.RegularExpressions;

namespace Store.Helpers;

public static class Validator
{
    public static bool isValidCode(string code)
    {
        return Regex.IsMatch(code, @"^.{10}$");
    }

    public static bool IsValidNif(string nif)
    {
        return Regex.IsMatch(nif, @"^\d{8}[A-Z]{1}$");
    }
}