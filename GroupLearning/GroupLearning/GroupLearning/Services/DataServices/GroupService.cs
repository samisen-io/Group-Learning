using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.EntityFrameworkCore;


namespace GroupLearning.Services.DataServices;
public class GroupService : IGroupService
{
  private readonly ApplicationDbContext _context;

  public GroupService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Group> GetGroupByIdAsync(int id)
  {
    return await _context.Groups.FindAsync(id);
  }

  public async Task<IEnumerable<Group>> GetAllGroupsAsync()
  {
    return await _context.Groups.ToListAsync();
  }

  public async Task<Group> CreateGroupAsync(Group group)
  {
    _context.Groups.Add(group);
    await _context.SaveChangesAsync();
    return group;
  }

  public async Task<Group> UpdateGroupAsync(Group group)
  {
    var existingGroup = await _context.Groups.FindAsync(group.Id);
    if (existingGroup == null)
    {
      return null; // Or throw an exception if preferred
    }

    existingGroup.Name = group.Name;
    existingGroup.Description = group.Description;

    existingGroup.UpdatedOn = DateTime.Now;
    _context.Groups.Update(existingGroup);
    await _context.SaveChangesAsync();
    return existingGroup;
  }

  public async Task<bool> DeleteGroupAsync(int id)
  {
    var group = await _context.Groups.FindAsync(id);
    if (group == null)
    {
      return false;
    }

    _context.Groups.Remove(group);
    await _context.SaveChangesAsync();
    return true;
  }
}


