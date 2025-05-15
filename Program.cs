using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Data;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.productRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.accountServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.FarmerServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
// Configure DbContext with SQLite
var dbName = builder.Configuration.GetConnectionString("DatabaseConnection")!
    .Split('=')[1];
var dbPath = Path.Combine(builder.Environment.ContentRootPath, dbName);
builder.Services.AddDbContext<applicationDBContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Register Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
// TODO: Register IFarmerRepository when you create it

// Register Services
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IAccountService, AccountService>();
// TODO: Register IFarmerService when you create it


// Register Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IFarmerRepository, FarmerRepository>();

// Register Services
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IFarmerServices, FarmerServices>();


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<applicationDBContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database.");
    }
}

app.Run();
