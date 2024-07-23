using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using Microsoft.EntityFrameworkCore;
using File = GroupLearning.Models.File;

namespace GroupLearning.Services.DataServices;

public class FileService : IFileService
{
  private readonly ApplicationDbContext _context;

  public FileService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<File> GetFileByIdAsync(int id)
  {
    return await _context.Files.FindAsync(id);
  }

  public async Task<IEnumerable<File>> GetAllFilesAsync()
  {
    return await _context.Files.ToListAsync();
  }

  public async Task<File> CreateFileAsync(File file)
  {
    _context.Files.Add(file);
    await _context.SaveChangesAsync();
    return file;
  }

  public async Task<File> UpdateFileAsync(File file)
  {
    var existingFile = await _context.Files.FindAsync(file.Id);
    if (existingFile == null)
    {
      return null; // Or throw an exception if preferred
    }

    existingFile.FileName = file.FileName;
    existingFile.UserId = file.UserId;

    existingFile.UploadedOn = file.UploadedOn;
    _context.Files.Update(existingFile);
    await _context.SaveChangesAsync();
    return existingFile;
  }

  public async Task<bool> DeleteFileAsync(int id)
  {
    var file = await _context.Files.FindAsync(id);
    if (file == null)
    {
      return false;
    }

    _context.Files.Remove(file);
    await _context.SaveChangesAsync();
    return true;
  }
}
