using GroupLearning.Extensions;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models.RequestModel;
using Microsoft.AspNetCore.Mvc;
using File = GroupLearning.Models.File;

namespace GroupLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
  private readonly IFileService _fileService;
  private readonly IUserService _UserService;
  private readonly IGroupService _GroupService;

  public FileController(IFileService fileService, IUserService userService, IGroupService groupService)
  {
    _fileService = fileService;
    _UserService = userService;
    _GroupService = groupService;
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
  public async Task<ActionResult<File>> CreateFile(IFormFile uploadFile, int userId, int groupId)
  {
    long fileSize = ((uploadFile.OpenReadStream().Length) / 1024) / 1024;

    if (fileSize > 5) throw new Exception("FileSize is greater than 5 MB");

    File file = new()
    {
      FileSize  = fileSize,
      UserId = userId,
      User = await _UserService.GetUserByIdAsync(userId),
      GroupId = groupId,
      Group = await _GroupService.GetGroupByIdAsync(groupId),
    };

    if (uploadFile == null || uploadFile.Length == 0)
    {
      return BadRequest("No file uploaded.");
    }

    using (var memoryStream = new MemoryStream())
    {
      await uploadFile.CopyToAsync(memoryStream);
      file.FileContent = memoryStream.ToArray();
      file.FileName = uploadFile.FileName;
      file.ContentType = uploadFile.ContentType;
    }

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
