using GroupLearning.Models;

namespace GroupLearning.Interfaces.DataServices
{
  public interface IGroupService
  {
    Task<Group> GetGroupByIdAsync(int id);
    Task<IEnumerable<Group>> GetAllGroupsAsync();
    Task<Group> CreateGroupAsync(Group group);
    Task<Group> UpdateGroupAsync(Group group);
    Task<bool> DeleteGroupAsync(int id);
  }
}
