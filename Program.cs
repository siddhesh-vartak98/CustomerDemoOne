using CustomerDemoOne.Common;
using CustomerDemoOne.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CustomerDemoOneDbContext>(options => options.UseSqlServer(Config.connectionString));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

builder.Services.AddSession(options=>options.IdleTimeout = TimeSpan.FromMinutes(30));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
