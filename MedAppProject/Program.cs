using MedAppProject.Data;
using MedAppProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using MedAppProject.Models;
using Microsoft.AspNetCore.Identity;
using MedAppProject.AutoRunningClasses;
using MedAppProject.ServicesClasses;

var builder = WebApplication.CreateBuilder(args);
//ttttttttttt
//test
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
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    // Set the session timeout value (optional)
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



//builder.Services.AddScoped<IMedAppRepository<VMLogin>, VMLoginRepository>();
builder.Services.AddScoped<IMedAppRepository<Doctor>, DoctorRepository>();
builder.Services.AddScoped<IMedAppRepository<Patient>, PatientRepository>();
builder.Services.AddScoped<IMedAppRepository<Lab>, LabRepository>();
builder.Services.AddScoped<IMedAppRepository<Test>, TestRepository>();
builder.Services.AddScoped<IMedAppRepository<DoctorAppointment>, DoctorAppointmentRepository>();
builder.Services.AddScoped<IMedAppRepository<LabAppointment>, LabAppointmentRepository>();
//builder.Services.AddScoped<IMedAppRepository<DoctorAvailableTimes>, DoctorAvailableTimesRepository>();
builder.Services.AddScoped(typeof(IMedAppRepository<>), typeof(MedAppRepository<>));
builder.Services.AddTransient<IEmailSender, EmailService>();
builder.Services.AddHostedService<MyBackgroundService>();


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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
