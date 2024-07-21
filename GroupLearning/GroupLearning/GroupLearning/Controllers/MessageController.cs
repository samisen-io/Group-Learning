using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
  private readonly IMessageService _messageService;

  public MessageController(IMessageService messageService)
  {
    _messageService = messageService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Message>> GetMessageById(int id)
  {
    var message = await _messageService.GetMessageByIdAsync(id);
    if (message == null)
    {
      return NotFound();
    }

    return Ok(message);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
  {
    var messages = await _messageService.GetAllMessagesAsync();
    return Ok(messages);
  }

  [HttpPost]
  public async Task<ActionResult<Message>> CreateMessage([FromBody] Message message)
  {
    var createdMessage = await _messageService.CreateMessageAsync(message);
    return CreatedAtAction(nameof(GetMessageById), new { id = createdMessage.Id }, createdMessage);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<Message>> UpdateMessage(int id, [FromBody] Message message)
  {
    if (id != message.Id)
    {
      return BadRequest();
    }

    var updatedMessage = await _messageService.UpdateMessageAsync(message);
    if (updatedMessage == null)
    {
      return NotFound();
    }

    return Ok(updatedMessage);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteMessage(int id)
  {
    var result = await _messageService.DeleteMessageAsync(id);
    if (!result)
    {
      return NotFound();
    }

    return NoContent();
  }
}
