using Microsoft.EntityFrameworkCore;
using System;
using Web_Blog.Data;
using Microsoft.AspNetCore.Identity;
using Web_Blog.Models.Interfaces;
using Web_Blog.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
//
builder.Services.AddScoped<IContactRepository, ContactRepository>();
// Register database
builder.Services.AddDbContext<WebblogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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

app.UseSession();

// Ti?p t?c v?i c�c middleware kh�c v� c?u h�nh


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
