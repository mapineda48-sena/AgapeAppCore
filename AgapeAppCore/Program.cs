using AgapeApp.Demo;
using AgapeApp.Infrastructure.Extensions;
using AgapeAppCore.WebApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddAppDbContext(builder.Configuration.GetConnectionString("Database"));

// Storage
builder.Services.AddStorageService(builder.Configuration.GetConnectionString("Storage"));

// Cache
builder.Services.AddCacheService(builder.Configuration.GetConnectionString("Cache"));

// Repositories
builder.Services.AddRepositories();

// Add services to the container.
// builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAntiforgery();
//app.UseRouting();

app.UseAuthorization();

//app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.InitializeDatabase();
app.InitializeDemo();

app.Run();
