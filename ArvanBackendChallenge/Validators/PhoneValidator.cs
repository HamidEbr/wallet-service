using System.Text.RegularExpressions;

namespace ArvanBackendChallenge.Validators;

public static partial class PhoneValidator
{
    static readonly Regex _mobilePhoneRegex = PhoneRegex();

    public static bool IsValidPhone(this string phone)
        => _mobilePhoneRegex.IsMatch(phone);

    [GeneratedRegex(@"^09[0-9]{9}$", RegexOptions.Compiled)]
    private static partial Regex PhoneRegex();
}
