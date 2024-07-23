using GroupLearning.Interfaces.OtpServices;

namespace GroupLearning.Services.OtpServices;

public class OtpService : IOtpService
{
  private readonly IOtpStoreService _otpStore;

  public OtpService(IOtpStoreService otpStore)
  {
    _otpStore = otpStore;
  }
  public string GenerateOtp(int length = 6)
  {
    const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    var random = new Random();
    var otp = new char[length];

    for (int i = 0; i < length; i++)
    {
      otp[i] = characters[random.Next(characters.Length)];
    }

    return new string(otp);
  }

  public bool VerifyOtp(string key, string otp)
  {
    Nullable<(string Otp, DateTime Expiry)> storedOtp = _otpStore.GetOtp(key);

    if (storedOtp.HasValue && storedOtp.Value.Otp == otp && storedOtp.Value.Expiry > DateTime.UtcNow)
    {
      _otpStore.RemoveOtp(key);
      return true;
    }

    return false;
  }
}

