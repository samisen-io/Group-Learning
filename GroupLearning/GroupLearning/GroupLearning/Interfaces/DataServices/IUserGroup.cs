using GroupLearning.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupLearning.Interfaces.DataServices;

public interface IUserGroupService
{
  Task<UserGroup> GetUserGroupAsync(int userId, int groupId);
  Task<IEnumerable<UserGroup>> GetAllUserGroupsAsync();
  Task<UserGroup> CreateUserGroupAsync(UserGroup userGroup);
  Task<bool> DeleteUserGroupAsync(int userId, int groupId);
}
