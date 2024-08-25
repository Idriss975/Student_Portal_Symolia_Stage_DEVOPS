using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;

var builder = WebApplication.CreateBuilder(args);
string MSSQL_SA_PASSWORD = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");

// Add services to the container.
builder.Services.AddControllersWithViews();

// This is what we add to inject our ApplicationDbContext into our application
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentPortal")+"Password="+MSSQL_SA_PASSWORD+";"));

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
/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

app.Run();

public partial class Program { }