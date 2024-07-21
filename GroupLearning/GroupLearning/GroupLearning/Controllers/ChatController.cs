using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
  private readonly IChatService _chatService;

  public ChatController(IChatService chatService)
  {
    _chatService = chatService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Chat>> GetChatById(int id)
  {
    var chat = await _chatService.GetChatByIdAsync(id);
    if (chat == null)
    {
      return NotFound();
    }

    return Ok(chat);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Chat>>> GetAllChats()
  {
    var chats = await _chatService.GetAllChatsAsync();
    return Ok(chats);
  }

  [HttpPost]
  public async Task<ActionResult<Chat>> CreateChat([FromBody] Chat chat)
  {
    var createdChat = await _chatService.CreateChatAsync(chat);
    return CreatedAtAction(nameof(GetChatById), new { id = createdChat.Id }, createdChat);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<Chat>> UpdateChat(int id, [FromBody] Chat chat)
  {
    if (id != chat.Id)
    {
      return BadRequest();
    }

    var updatedChat = await _chatService.UpdateChatAsync(chat);
    if (updatedChat == null)
    {
      return NotFound();
    }

    return Ok(updatedChat);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteChat(int id)
  {
    var result = await _chatService.DeleteChatAsync(id);
    if (!result)
    {
      return NotFound();
    }

    return NoContent();
  }
}
