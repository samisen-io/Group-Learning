using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using GroupLearning.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace GroupLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost("login")]
  public async Task<ActionResult<User>> Login([FromBody] LoginModel loginModel)
  {
    var user = await _userService.GetUserByCredentialsAsync(loginModel.Email, loginModel.Password);
    if (user == null)
    {
      return Unauthorized();
    }

    return Ok(user);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<User>> GetUserById(int id)
  {
    var user = await _userService.GetUserByIdAsync(id);
    if (user == null)
    {
      return NotFound();
    }

    return Ok(user);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
  {
    var users = await _userService.GetAllUsersAsync();
    return Ok(users);
  }

  [HttpPost]
  public async Task<ActionResult<User>> CreateUser([FromBody] UserRequestModel userRequest)
  {
    try
    {
      User user = new()
      {
        FirstName = userRequest.FirstName,
        LastName = userRequest.LastName,
        Email = userRequest.Email,
        PasswordHash = userRequest.Password,
        PhoneNumber = userRequest.PhoneNumber,
      };

      var createdUser = await _userService.CreateUserAsync(user);
      return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
    }
    catch (Exception)
    {

      throw;
    }

  }

  [HttpPut("{id}")]
  public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
  {
    if (id != user.Id)
    {
      return BadRequest();
    }

    var updatedUser = await _userService.UpdateUserAsync(user);
    if (updatedUser == null)
    {
      return NotFound();
    }

    return Ok(updatedUser);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteUser(int id)
  {
    var result = await _userService.DeleteUserAsync(id);
    if (!result)
    {
      return NotFound();
    }

    return NoContent();
  }
}
