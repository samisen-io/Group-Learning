using System.Text.RegularExpressions;

namespace GroupLearning.Extensions;

public static class StringExtension
{
  public static bool IsValidEmail(this string email)
  {
    var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
    return emailRegex.IsMatch(email);
  }

  public static bool IsValidPhoneNumber(this string phoneNumber)
  {
    var phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$");
    return phoneRegex.IsMatch(phoneNumber);
  }
}
