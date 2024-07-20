using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupLearning.Services.DataServices;

public class AppService : IAppService
{
  private readonly ApplicationDbContext _appDbContext;

  public AppService(ApplicationDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }

  // Insert
  public async Task<App> InsertAppAsync(App app)
  {
    _appDbContext.App.Add(app);
    await _appDbContext.SaveChangesAsync();
    return app;
  }

  // Update
  public async Task<App> UpdateAppAsync(App app)
  {
    var existingApp = await _appDbContext.App.FindAsync(app.Id);
    if (existingApp != null)
    {
      // Update properties
      existingApp.Name = app.Name;
      existingApp.Description = app.Description;
      // Add more properties as needed

      await _appDbContext.SaveChangesAsync();
      return existingApp;
    }

    return null;
  }

  // Delete
  public async Task<bool> DeleteAppAsync(int id)
  {
    var app = await _appDbContext.App.FindAsync(id);
    if (app != null)
    {
      _appDbContext.App.Remove(app);
      await _appDbContext.SaveChangesAsync();
      return true;
    }

    return false;
  }

  // Select (Get by ID)
  public async Task<App> GetAppByIdAsync(int id)
  {
    return await _appDbContext.App.FindAsync(id) ?? throw new Exception("No Content");
  }

  // Select (Get all)
  public async Task<IEnumerable<App>> GetAllAppsAsync()
  {
    return await _appDbContext.App.ToListAsync();
  }
}



