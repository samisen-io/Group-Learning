using GroupLearning.Interfaces.DataServices;
using Microsoft.AspNetCore.Mvc;
using File = GroupLearning.Models.File;

namespace GroupLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
  private readonly IFileService _fileService;

  public FileController(IFileService fileService)
  {
    _fileService = fileService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<File>> GetFileById(int id)
  {
    var file = await _fileService.GetFileByIdAsync(id);
    if (file == null)
    {
      return NotFound();
    }

    return Ok(file);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<File>>> GetAllFiles()
  {
    var files = await _fileService.GetAllFilesAsync();
    return Ok(files);
  }

  [HttpPost]
  public async Task<ActionResult<File>> CreateFile([FromBody] File file)
  {
    var createdFile = await _fileService.CreateFileAsync(file);
    return CreatedAtAction(nameof(GetFileById), new { id = createdFile.Id }, createdFile);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<File>> UpdateFile(int id, [FromBody] File file)
  {
    if (id != file.Id)
    {
      return BadRequest();
    }

    var updatedFile = await _fileService.UpdateFileAsync(file);
    if (updatedFile == null)
    {
      return NotFound();
    }

    return Ok(updatedFile);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteFile(int id)
  {
    var result = await _fileService.DeleteFileAsync(id);
    if (!result)
    {
      return NotFound();
    }

    return NoContent();
  }
}
