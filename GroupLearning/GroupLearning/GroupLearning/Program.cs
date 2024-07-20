using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Services.DataServices;
using Microsoft.EntityFrameworkCore;
using App = GroupLearning.Components.App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
  builder.Configuration.GetConnectionString("localDb")));

// Change to AddScoped for proper handling of DbContext lifecycle
builder.Services.AddScoped<IAppService, AppService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserGroupService, UserGroupService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error", createScopeForErrors: true);
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GroupLearning.Client._Imports).Assembly);

// Create a scope for service resolution and usage
using (var scope = app.Services.CreateScope())
{
  var scopedServices = scope.ServiceProvider;
  var appService = scopedServices.GetRequiredService<IAppService>();
  var appModel = await appService.InsertAppAsync(new GroupLearning.Models.App()
  {
    Name = "app",
    Description = "app",
  });

  var getAppModel = await appService.GetAppByIdAsync(appModel.Id);
}

app.Run();
