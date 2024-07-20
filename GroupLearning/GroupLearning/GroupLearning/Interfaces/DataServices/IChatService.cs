using GroupLearning.Models;

namespace GroupLearning.Interfaces.DataServices;

public interface IChatService
{
  Task<Chat> GetChatByIdAsync(int id);
  Task<IEnumerable<Chat>> GetAllChatsAsync();
  Task<Chat> CreateChatAsync(Chat chat);
  Task<Chat> UpdateChatAsync(Chat chat);
  Task<bool> DeleteChatAsync(int id);
}
