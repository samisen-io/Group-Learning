﻿namespace GroupLearning.Interfaces.EmailServices;

public interface IEmailService
{
  Task SendOtpAsync(string email, string otp);
}
