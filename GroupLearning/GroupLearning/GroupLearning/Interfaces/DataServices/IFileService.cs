using File = GroupLearning.Models.File;

namespace GroupLearning.Interfaces.DataServices;

public interface IFileService
{
  Task<File> GetFileByIdAsync(int id);
  Task<IEnumerable<File>> GetAllFilesAsync();
  Task<File> CreateFileAsync(File file);
  Task<File> UpdateFileAsync(File file);
  Task<bool> DeleteFileAsync(int id);
}
