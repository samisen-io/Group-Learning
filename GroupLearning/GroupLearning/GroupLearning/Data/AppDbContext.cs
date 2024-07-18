using GroupLearning.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupLearning.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<App> App { get; set; }
}
