using GroupLearning.Interfaces.EmailServices;
using System.Net.Mail;

namespace GroupLearning.Services.EmailServices;

public class EmailService : IEmailService
{
  private readonly SmtpClient _smtpClient;

  public EmailService(SmtpClient smtpClient)
  {
    _smtpClient = smtpClient;
  }

  public async Task SendOtpAsync(string email, string otp)
  {
    var mailMessage = new MailMessage("demo34125@gmail.com", email)
    {
      Subject = "Your OTP Code",
      Body = $"Your OTP code is: {otp}"
    };

    await _smtpClient.SendMailAsync(mailMessage);
  }
}

