using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserGroupController : ControllerBase
{
  private readonly IUserGroupService _userGroupService;

  public UserGroupController(IUserGroupService userGroupService)
  {
    _userGroupService = userGroupService;
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
  public async Task<ActionResult<UserGroup>> CreateUserGroup([FromBody] UserGroup userGroup)
  {
    var createdUserGroup = await _userGroupService.CreateUserGroupAsync(userGroup);
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
