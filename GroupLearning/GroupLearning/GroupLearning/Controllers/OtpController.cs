using GroupLearning.Extensions;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Interfaces.EmailServices;
using GroupLearning.Interfaces.OneSignalServices;
using GroupLearning.Interfaces.OtpServices;
using GroupLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupLearning.Controllers;

public class OtpController : ControllerBase
{
  private readonly IOtpService _otpService;
  private readonly IOtpStoreService _otpStoreService;
  private readonly IEmailService _emailService;
  private readonly IUserService _userService;
  private readonly IOneSignalService _oneSignalService;

  public OtpController(IOtpService otpService, IOtpStoreService otpStoreService, IEmailService emailService, IUserService userService, IOneSignalService oneSignalService)
  {
    _otpStoreService = otpStoreService;
    _emailService = emailService;
    _otpService = otpService;
    _userService = userService;
    _oneSignalService = oneSignalService;
  }

  [HttpPost("send-otp")]
  public async Task<IActionResult> SendOtp(string email, string phoneNumber)
  {
    if (!string.IsNullOrEmpty(email))
    {
      string otpEmail = _otpService.GenerateOtp();
      _otpStoreService.StoreOtp(email, otpEmail);
      await _emailService.SendOtpAsync(email, otpEmail);
    }
    if (!string.IsNullOrEmpty(phoneNumber))
    {
      string otpPhone = _otpService.GenerateOtp();
      _otpStoreService.StoreOtp(phoneNumber, otpPhone);
      await _oneSignalService.SendSmsAsync(phoneNumber, $"The User Otp : {otpPhone}");
    }
    else throw new Exception("The email and phone number should not be empty or null. ");

    return Ok("OTP sent successfully.");
  }

  [HttpPost("verify-otp")]
  public async Task<IActionResult> VerifyOtp(string emailOrPhoneNumber, string otp)
  {
    var isValid = _otpService.VerifyOtp(emailOrPhoneNumber, otp);

    if (isValid)
    {
      if (emailOrPhoneNumber.IsValidEmail())
      {
        User user = await _userService.GetUserByEmail(emailOrPhoneNumber);
        user.IsEmailVerified = true;

        await _userService.UpdateUserAsync(user);

        return Ok("OTP verified successfully.");
      }
      else if (emailOrPhoneNumber.IsValidPhoneNumber())
      {
        User user = await _userService.GetUserByPhoneNumber(emailOrPhoneNumber);
        user.IsPhoneNumberVerified = true;

        await _userService.UpdateUserAsync(user);

        return Ok("OTP verified successfully.");
      }
      return Ok("OTP verified successfully.");
    }

    return BadRequest("Invalid OTP.");
  }
}

