using ConnectDatabase.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//add services to the web container
//mvvm structure
builder.Services.AddRazorPages();

//declare session
builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(15));

//add DBContext object
builder.Services.AddDbContext<PRNDBContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("PRNDB"));
});

var app = builder.Build();

//declare libs usage in wwwroot
app.UseStaticFiles();

//use session
app.UseSession();

//mapping 
app.MapRazorPages();

app.Run();
