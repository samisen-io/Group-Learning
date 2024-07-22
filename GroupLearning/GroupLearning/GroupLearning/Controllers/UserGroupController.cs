using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using GroupLearning.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace GroupLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserGroupController : ControllerBase
{
  private readonly IUserGroupService _userGroupService;
  private readonly IUserService _userService;
  private readonly IGroupService _groupService;

  public UserGroupController(IUserGroupService userGroupService,
                             IUserService userService,
                             IGroupService groupService)
  {
    _userGroupService = userGroupService;
    _userService = userService;
    _groupService = groupService;
  }

  [HttpGet("{userId}/{groupId}")]
  public async Task<ActionResult<UserGroup>> GetUserGroup(int userId, int groupId)
  {
    var userGroup = await _userGroupService.GetUserGroupAsync(userId, groupId);
    if (userGroup == null)
    {
      return NotFound();
    }

    return Ok(userGroup);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<UserGroup>>> GetAllUserGroups()
  {
    var userGroups = await _userGroupService.GetAllUserGroupsAsync();
    return Ok(userGroups);
  }

  [HttpPost]
  public async Task<ActionResult<UserGroup>> CreateUserGroup([FromBody] UserGroupRequestModel userGroupRequest)
  {
    User user = await _userService.GetUserByIdAsync(userGroupRequest.UserId);
    Group group = await _groupService.GetGroupByIdAsync(userGroupRequest.GroupId);
    UserGroup createdUserGroup = await _userGroupService.CreateUserGroupAsync(new UserGroup
    {
      GroupId = userGroupRequest.GroupId,
      Group = group,
      UserId = userGroupRequest.UserId,
      User = user,
    });

    return CreatedAtAction(nameof(GetUserGroup), new { userId = createdUserGroup.UserId, groupId = createdUserGroup.GroupId }, createdUserGroup);
  }

  [HttpDelete("{userId}/{groupId}")]
  public async Task<IActionResult> DeleteUserGroup(int userId, int groupId)
  {
    var result = await _userGroupService.DeleteUserGroupAsync(userId, groupId);
    if (!result)
    {
      return NotFound();
    }

    return NoContent();
  }
}
