namespace GroupLearning.Interfaces.OtpServices;

public interface IOtpStoreService
{
  void StoreOtp(string key, string otp, int expiryMinutes = 5);
  (string Otp, DateTime Expiry)? GetOtp(string key);
  void RemoveOtp(string key);
}
