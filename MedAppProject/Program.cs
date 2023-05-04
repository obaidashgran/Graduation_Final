using MedAppProject.Data;
using MedAppProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using MedAppProject.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseSqlServer
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseConecction"));
});
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Access/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
//builder.Services.AddScoped<IMedAppRepository<VMLogin>, VMLoginRepository>();
builder.Services.AddScoped<IMedAppRepository<Doctor>, PatientRepository>();
builder.Services.AddScoped(typeof(IMedAppRepository<>), typeof(MedAppRepository<>));
//builder.Services.AddSingleton<VMLoginRepository>();
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
