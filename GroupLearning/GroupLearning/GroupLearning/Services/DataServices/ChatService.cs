using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupLearning.Services.DataServices;

public class ChatService : IChatService
{
  private readonly ApplicationDbContext _context;

  public ChatService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Chat> GetChatByIdAsync(int id)
  {
    return await _context.Chats
        .Include(c => c.Group)
        .FirstOrDefaultAsync(c => c.Id == id);
  }

  public async Task<IEnumerable<Chat>> GetAllChatsAsync()
  {
    return await _context.Chats
        .Include(c => c.Group)
        .ToListAsync();
  }

  public async Task<Chat> CreateChatAsync(Chat chat)
  {
    _context.Chats.Add(chat);
    await _context.SaveChangesAsync();
    return chat;
  }

  public async Task<Chat> UpdateChatAsync(Chat chat)
  {
    var existingChat = await _context.Chats.FindAsync(chat.Id);
    if (existingChat == null)
    {
      return null;
    }

    existingChat.GroupId = chat.GroupId;

    _context.Chats.Update(existingChat);
    await _context.SaveChangesAsync();
    return existingChat;
  }

  public async Task<bool> DeleteChatAsync(int id)
  {
    var chat = await _context.Chats.FindAsync(id);
    if (chat == null)
    {
      return false;
    }

    _context.Chats.Remove(chat);
    await _context.SaveChangesAsync();
    return true;
  }
}

