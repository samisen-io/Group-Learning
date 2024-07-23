namespace GroupLearning.Interfaces.OneSignalServices;

public interface IOneSignalService
{
  Task SendSmsAsync(string phoneNumber, string message);
}
