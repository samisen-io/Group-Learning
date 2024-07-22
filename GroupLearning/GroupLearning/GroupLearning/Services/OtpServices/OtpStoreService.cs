using GroupLearning.Interfaces.OtpServices;

namespace GroupLearning.Services.OtpServices;

public class OtpStoreService : IOtpStoreService
{
  private readonly Dictionary<string, (string Otp, DateTime Expiry)> _otpStorage = [];

  public void StoreOtp(string key, string otp, int expiryMinutes = 5)
  {
    DateTime expiryTime = DateTime.UtcNow.AddMinutes(expiryMinutes);
    _otpStorage[key] = (otp, expiryTime);
  }

  public (string Otp, DateTime Expiry)? GetOtp(string key)
  {
    if (_otpStorage.TryGetValue(key, out var value))
    {
      return value;
    }

    return null;
  }

  public void RemoveOtp(string key)
  {
    _otpStorage.Remove(key);
  }
}

