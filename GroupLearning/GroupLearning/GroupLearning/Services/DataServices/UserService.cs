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

  public async Task<User> GetUserByEmail(string email)
  {
    User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    if (user == null)
    {
      return null;
    }

    return user;
  }

  public async Task<User> GetUserByPhoneNumber(string phoneNumber)
  {
    User? user = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

    if (user == null)
    {
      return null;
    }

    return user;
  }

  public async Task<User> GetUserByCredentialsAsync(string email, string password)
  {
    // Find user by username
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
    {
      return null;
    }

    return user;
  }

  public async Task<User> GetUserByIdAsync(int id)
  {
    return await _context.Users.FindAsync(id);
  }

  public async Task<IEnumerable<User>> GetAllUsersAsync()
  {
    return await _context.Users.ToListAsync();
  }

  public async Task<User> CreateUserAsync(User user)
  {
    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return user;
  }

  public async Task<User> UpdateUserAsync(User user)
  {
    var existingUser = await _context.Users.FindAsync(user.Id);
    if (existingUser == null)
    {
      return null; // Or throw an exception if preferred
    }

    existingUser.FirstName = user.FirstName;
    existingUser.LastName = user.LastName;
    existingUser.Email = user.Email;

    existingUser.UpdatedOn = DateTime.UtcNow;

    _context.Users.Update(existingUser);
    await _context.SaveChangesAsync();
    return existingUser;
  }

  public async Task<bool> DeleteUserAsync(int id)
  {
    var user = await _context.Users.FindAsync(id);
    if (user == null)
    {
      return false;
    }

    _context.Users.Remove(user);
    await _context.SaveChangesAsync();
    return true;
  }
}
