using GroupLearning.Models;

namespace GroupLearning.Interfaces.DataServices;
public interface IMessageService
{
  Task<Message> GetMessageByIdAsync(int id);
  Task<IEnumerable<Message>> GetAllMessagesAsync();
  Task<Message> CreateMessageAsync(Message message);
  Task<Message> UpdateMessageAsync(Message message);
  Task<bool> DeleteMessageAsync(int id);
}
