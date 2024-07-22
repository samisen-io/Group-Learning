using GroupLearning.Extensions;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Interfaces.EmailServices;
using GroupLearning.Interfaces.OtpServices;
using GroupLearning.Interfaces.SmsServices;
using GroupLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupLearning.Controllers;

public class OtpController : ControllerBase
{
  private readonly IOtpService _otpService;
  private readonly IOtpStoreService _otpStoreService;
  private readonly IEmailService _emailService;
  private readonly ISmsService _smsService;
  private readonly IUserService _userService;

  public OtpController(IOtpService otpService, IOtpStoreService otpStoreService, IEmailService emailService, ISmsService smsService, IUserService userService)
  {
    _otpStoreService = otpStoreService;
    _emailService = emailService;
    _smsService = smsService;
    _otpService = otpService;
    _userService = userService;
  }

  [HttpPost("send-otp")]
  public async Task<IActionResult> SendOtp(string email, string phoneNumber)
  {
    string otpEmail = _otpService.GenerateOtp();
    _otpStoreService.StoreOtp(email, otpEmail);

    string otpPhone = _otpService.GenerateOtp();
    _otpStoreService.StoreOtp(phoneNumber, otpPhone);

    await _emailService.SendOtpAsync(email, otpEmail);
    await _smsService.SendOtpAsync(phoneNumber, otpPhone);

    return Ok("OTP sent successfully.");
  }

  [HttpPost("verify-otp")]
  public async Task<IActionResult> VerifyOtp(string key, string otp)
  {
    var isValid = _otpService.VerifyOtp(key, otp);

    if (isValid)
    {
      if (key.IsValidEmail())
      {
        User user = await _userService.GetUserByEmail(key);
        user.IsEmailVerified = true;

        await _userService.UpdateUserAsync(user);

        return Ok("OTP verified successfully.");
      }
      else if (key.IsValidPhoneNumber())
      {
        User user = await _userService.GetUserByPhoneNumber(key);
        user.IsPhoneNumberVerified = true;

        await _userService.UpdateUserAsync(user);

        return Ok("OTP verified successfully.");
      }
      return Ok("OTP verified successfully.");
    }

    return BadRequest("Invalid OTP.");
  }
}

