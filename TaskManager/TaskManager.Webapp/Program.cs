using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManager.Application.Infrastructure;
using TaskManager.Application.Repository;
using TaskManager.Application.Service;

var builder = WebApplication.CreateBuilder(args);

// Configure session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb");
var mongoDatabaseName = builder.Configuration["MongoDbDatabaseName"];
builder.Services.AddSingleton<TasksContext>(sp =>
    new TasksContext(mongoConnectionString, mongoDatabaseName));

// Repository services
builder.Services.AddScoped<UserRepository>(provider =>
    new UserRepository(provider.GetRequiredService<TasksContext>().Users));
builder.Services.AddScoped<ProfileRepository>(provider =>
    new ProfileRepository(provider.GetRequiredService<TasksContext>().Profiles));
builder.Services.AddScoped<TaskRepository>(provider =>
    new TaskRepository(provider.GetRequiredService<TasksContext>().Tasks));
builder.Services.AddScoped<SubjectRepository>(provider =>
    new SubjectRepository(provider.GetRequiredService<TasksContext>().Subjects));
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapRazorPages();
app.Run();