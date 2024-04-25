using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web_Blog.Areas.Identity.Data;
using Web_Blog.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Web_BlogDBContextConnection") ?? throw new InvalidOperationException("Connection string 'Web_BlogDBContextConnection' not found.");

builder.Services.AddDbContext<Web_BlogDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Web_BlogUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<Web_BlogDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
// Ch? này là tui thêm các d?ch v? cho Razor Pages vào ?ng d?ng á nhen (Huynh)
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
