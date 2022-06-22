using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Net6ReactApp.Data;
using Net6ReactApp.Models;
using NLog;
using NLog.Web;

// Initialize NLog
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Initializing Program");

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
	builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(connectionString));
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();

	builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
		.AddEntityFrameworkStores<ApplicationDbContext>();

	builder.Services.AddIdentityServer()
		.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

	builder.Services.AddAuthentication()
		.AddIdentityServerJwt();

	builder.Services.AddControllersWithViews();
	builder.Services.AddRazorPages();

	// Setup NLog for Dependency injection
	builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
	builder.Host.UseNLog();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseMigrationsEndPoint();
	}
	else
	{
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseHttpsRedirection();
	app.UseStaticFiles();
	app.UseRouting();

	app.UseAuthentication();
	app.UseIdentityServer();
	app.UseAuthorization();

	app.MapControllerRoute(
		name: "default",
		pattern: "{controller}/{action=Index}/{id?}");
	app.MapRazorPages();

	app.MapFallbackToFile("index.html"); ;

	app.Run();
}
catch (Exception exception)
{
	logger.Error(exception, "Execution stoped, due to Exception");
	throw;
}
finally
{
	LogManager.Shutdown();
}