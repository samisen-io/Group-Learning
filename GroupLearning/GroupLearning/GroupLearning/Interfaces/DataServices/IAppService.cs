using GroupLearning.Models;

namespace GroupLearning.Interfaces.DataServices;

public interface IAppService
{
  Task<App> InsertAppAsync(App app);
  Task<App> UpdateAppAsync(App app);
  Task<bool> DeleteAppAsync(int id);
  Task<App> GetAppByIdAsync(int id);
  Task<IEnumerable<App>> GetAllAppsAsync();
}
