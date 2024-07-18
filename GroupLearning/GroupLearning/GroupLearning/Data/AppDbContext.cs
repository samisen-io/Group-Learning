using GroupLearning.Models;
using Microsoft.EntityFrameworkCore;

namespace GroupLearning.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<App> App { get; set; }
}
