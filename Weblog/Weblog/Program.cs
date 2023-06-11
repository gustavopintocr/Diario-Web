using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Controllers;
using Weblog.Data;
using Weblog.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WeblogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WeblogContext") ?? throw new InvalidOperationException("Connection string 'WeblogContext' not found.")));

builder.Services.AddIdentity<User, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<WeblogContext>()
    .AddDefaultUI();

builder.Services.AddMvc().AddApplicationPart(typeof(HomeController).Assembly)
            .AddControllersAsServices();

builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
