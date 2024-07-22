using GroupLearning.Interfaces.SmsServices;

namespace GroupLearning.Services.SmsServices;

public class SmsService : ISmsService
{
  private readonly string _accountSid;
  private readonly string _authToken;
  private readonly string _fromPhoneNumber;

  public SmsService(string accountSid, string authToken, string fromPhoneNumber)
  {
    _accountSid = accountSid;
    _authToken = authToken;
    _fromPhoneNumber = fromPhoneNumber;
  }

  public async Task SendOtpAsync(string phoneNumber, string otp)
  {
    await Task.Yield();
    //TwilioClient.Init(_accountSid, _authToken);

    //var message = await MessageResource.CreateAsync(
    //    body: $"Your OTP code is: {otp}",
    //    from: new Twilio.Types.PhoneNumber(_fromPhoneNumber),
    //    to: new Twilio.Types.PhoneNumber(phoneNumber)
    //);
  }
}

