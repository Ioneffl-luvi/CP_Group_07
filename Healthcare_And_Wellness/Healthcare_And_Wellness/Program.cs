using Healthcare_And_Wellness.Data;
using Microsoft.EntityFrameworkCore;
using DinkToPdf;
using QuestPDF.Infrastructure;
using DinkToPdf.Contracts;

var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.AddSession(Options => { Options.IdleTimeout = TimeSpan.FromSeconds(200); });
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ManagementContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ManagementContext")));
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();

