using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupLearning.Services.DataServices;
public class MessageService : IMessageService
{
  private readonly ApplicationDbContext _context;

  public MessageService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Message> GetMessageByIdAsync(int id)
  {
    return await _context.Messages
        .Include(m => m.Chat)
        .FirstOrDefaultAsync(m => m.Id == id);
  }

  public async Task<IEnumerable<Message>> GetAllMessagesAsync()
  {
    return await _context.Messages
        .Include(m => m.Chat)
        .ToListAsync();
  }

  public async Task<Message> CreateMessageAsync(Message message)
  {
    _context.Messages.Add(message);
    await _context.SaveChangesAsync();
    return message;
  }

  public async Task<Message> UpdateMessageAsync(Message message)
  {
    var existingMessage = await _context.Messages.FindAsync(message.Id);
    if (existingMessage == null)
    {
      return null; // Or throw an exception if preferred
    }

    existingMessage.Content = message.Content;

    _context.Messages.Update(existingMessage);
    await _context.SaveChangesAsync();
    return existingMessage;
  }

  public async Task<bool> DeleteMessageAsync(int id)
  {
    var message = await _context.Messages.FindAsync(id);
    if (message == null)
    {
      return false;
    }

    _context.Messages.Remove(message);
    await _context.SaveChangesAsync();
    return true;
  }
}

