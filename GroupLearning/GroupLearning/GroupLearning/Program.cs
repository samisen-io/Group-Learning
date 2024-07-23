using GroupLearning.Data;
using GroupLearning.Interfaces.DataServices;
using GroupLearning.Interfaces.EmailServices;
using GroupLearning.Interfaces.OtpServices;
using GroupLearning.Interfaces.SmsServices;
using GroupLearning.Services.DataServices;
using GroupLearning.Services.EmailServices;
using GroupLearning.Services.OtpServices;
using GroupLearning.Services.SmsServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;
using App = GroupLearning.Components.App;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
  .AddJsonOptions(options =>
   {
     options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
   });
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(
  builder.Configuration.GetConnectionString("localDb")));

// Change to AddScoped for proper handling of DbContext lifecycle

//AddSingleTon
builder.Services.AddSingleton<IOtpStoreService, OtpStoreService>();

//AddScoped
builder.Services.AddScoped<IAppService, AppService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserGroupService, UserGroupService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IFileService, FileService>();

//AddTransient
builder.Services.AddTransient<IOtpService, OtpService>();
builder.Services.AddTransient<IEmailService, EmailService>(provider =>
{
  SmtpClient smtpClient = new("smtp.gmail.com", 587)
  {
    Credentials = new NetworkCredential("demo34125@gmail.com", "orse wxwr crjv sxry"),
    EnableSsl = true
  };
  return new EmailService(smtpClient);
});
builder.Services.AddTransient<ISmsService, SmsService>(provider =>
    new SmsService("your-twilio-account-sid", "your-twilio-auth-token", "your-twilio-phone-number"));


// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "GroupLearning API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
  app.UseSwagger();
  app.UseSwaggerUI(c =>
  {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GroupLearning API v1");
    c.RoutePrefix = "Swagger"; // To serve the Swagger UI at the app's root (http://localhost:<port>/)
  });
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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


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
