using GroupLearning.Models;
using Microsoft.EntityFrameworkCore;
using File = GroupLearning.Models.File;

namespace GroupLearning.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
  public DbSet<App> App { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Group> Groups { get; set; }
  public DbSet<UserGroup> UserGroups { get; set; }
  public DbSet<Chat> Chats { get; set; }
  public DbSet<Message> Messages { get; set; }
  public DbSet<File> Files { get; set; }
}
