using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupLearning.Services.DataServices;

public class UserService : IUserService
{
  private readonly ApplicationDbContext _context;

  public UserService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<User> GetUserByCredentialsAsync(string email, string password)
  {
    // Find user by username
    var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
    if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
    {
      return null;
    }

    return user;
  }

  public async Task<User> GetUserByIdAsync(int id)
  {
    return await _context.User.FindAsync(id);
  }

  public async Task<IEnumerable<User>> GetAllUsersAsync()
  {
    return await _context.User.ToListAsync();
  }

  public async Task<User> CreateUserAsync(User user)
  {
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

    _context.User.Add(user);
    await _context.SaveChangesAsync();
    return user;
  }

  public async Task<User> UpdateUserAsync(User user)
  {
    var existingUser = await _context.User.FindAsync(user.Id);
    if (existingUser == null)
    {
      return null; // Or throw an exception if preferred
    }

    existingUser.FirstName = user.FirstName;
    existingUser.LastName = user.LastName;
    existingUser.Email = user.Email;

    existingUser.UpdatedOn = DateTime.UtcNow;

    _context.User.Update(existingUser);
    await _context.SaveChangesAsync();
    return existingUser;
  }

  public async Task<bool> DeleteUserAsync(int id)
  {
    var user = await _context.User.FindAsync(id);
    if (user == null)
    {
      return false;
    }

    _context.User.Remove(user);
    await _context.SaveChangesAsync();
    return true;
  }
}
