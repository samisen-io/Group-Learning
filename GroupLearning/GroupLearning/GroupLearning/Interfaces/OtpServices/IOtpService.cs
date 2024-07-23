namespace GroupLearning.Interfaces.OtpServices;

public interface IOtpService
{
  string GenerateOtp(int length = 6);
  bool VerifyOtp(string key, string otp);
}
