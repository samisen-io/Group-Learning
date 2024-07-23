using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using GroupLearning.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace GroupLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
  private readonly IGroupService _groupService;

  public GroupController(IGroupService groupService)
  {
    _groupService = groupService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Group>> GetGroupById(int id)
  {
    var group = await _groupService.GetGroupByIdAsync(id);
    if (group == null)
    {
      return NotFound();
    }

    return Ok(group);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Group>>> GetAllGroups()
  {
    var groups = await _groupService.GetAllGroupsAsync();
    return Ok(groups);
  }

  [HttpPost]
  public async Task<ActionResult<Group>> CreateGroup([FromBody] GroupRequestModel groupRequest)
  {
    var createdGroup = await _groupService.CreateGroupAsync(new Group
    {
      Name = groupRequest.Name, Description = groupRequest.Description
    });

    return CreatedAtAction(nameof(GetGroupById), new { id = createdGroup.Id }, createdGroup);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<Group>> UpdateGroup(int id, [FromBody] Group group)
  {
    if (id != group.Id)
    {
      return BadRequest();
    }

    var updatedGroup = await _groupService.UpdateGroupAsync(group);
    if (updatedGroup == null)
    {
      return NotFound();
    }

    return Ok(updatedGroup);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteGroup(int id)
  {
    var result = await _groupService.DeleteGroupAsync(id);
    if (!result)
    {
      return NotFound();
    }

    return NoContent();
  }
}
