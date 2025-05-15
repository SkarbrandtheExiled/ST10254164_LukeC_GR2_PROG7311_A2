using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.productRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.employeeServices;
using System;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

 builder.Services.AddScoped<IFarmerRepository, FarmerRepository>();
  builder.Services.AddScoped<IFarmerServices, FarmerServices>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
 builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddDbContext<applicationDBContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddSession();

var app = builder.Build();

app.UseSession();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
