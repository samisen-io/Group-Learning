using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupLearning.Services.DataServices;

public class UserGroupService : IUserGroupService
{
  private readonly ApplicationDbContext _context;

  public UserGroupService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<UserGroup> GetUserGroupAsync(int userId, int groupId)
  {
    return await _context.UserGroups
        .Include(c => c.Group)
        .Include(c => c.User)
        .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);
  }

  public async Task<IEnumerable<UserGroup>> GetAllUserGroupsAsync()
  {
    return await _context.UserGroups
        .ToListAsync();
  }

  public async Task<UserGroup> CreateUserGroupAsync(UserGroup userGroup)
  {
    _context.UserGroups.Add(userGroup);
    await _context.SaveChangesAsync();
    return userGroup;
  }

  public async Task<bool> DeleteUserGroupAsync(int userId, int groupId)
  {
    var userGroup = await _context.UserGroups
        .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);

    if (userGroup == null)
    {
      return false;
    }

    _context.UserGroups.Remove(userGroup);
    await _context.SaveChangesAsync();
    return true;
  }
}
