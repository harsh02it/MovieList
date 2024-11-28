using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieList.Models;
using System;
using Microsoft.AspNetCore.Identity;
using MovieList.Models.Data;
using MovieList.Middleware;
using Microsoft.AspNetCore.Hosting;

namespace MovieList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<MovieContext>(options =>
                            options.UseSqlServer(context.Configuration.GetConnectionString("MovieContext"))
                        );

                        services.AddDefaultIdentity<IdentityUser>(options =>
                        {
                            // SignIn settings
                            options.SignIn.RequireConfirmedAccount = false;
                        })
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<MovieContext>();

                        services.AddControllersWithViews();

                        services.AddRouting(options =>
                        {
                            options.LowercaseUrls = true;
                            options.AppendTrailingSlash = true;
                        });

                        services.AddSession(options =>
                        {
                            options.IdleTimeout = TimeSpan.FromSeconds(60 * 0.5);
                            options.Cookie.HttpOnly = false;
                            options.Cookie.IsEssential = true;
                        });

                        services.ConfigureApplicationCookie(options =>
                        {
                            options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                            options.SlidingExpiration = true;
                            options.LoginPath = "/Identity/Account/Login";
                            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                        });

                    })
                    .Configure((context, app) =>
                    {
                        var env = context.HostingEnvironment;
                        if (env.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                            app.UseHsts();
                        }

                        app.UseHttpsRedirection();
                        app.UseStaticFiles();
                        app.UseRouting();

                        app.UseAuthentication();
                        app.UseAuthorization();
                        app.UseSession();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}"
                            );
                            endpoints.MapAreaControllerRoute(
                                name: "Identity",
                                areaName: "Identity",
                                pattern: "Identity/{controller=Account}/{action=Login}/{id?}"
                            );
                            endpoints.MapRazorPages();
                        });
                    });
                });
    }
}



//var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("MovieContext") ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.");

//builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<MovieContext>();

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//// Configure the database context with the connection string.
//builder.Services.AddDbContext<MovieContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieContext")));

//// Add session services
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(15);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

//// Configure routing options.
//builder.Services.AddRouting(options =>
//{
//    options.LowercaseUrls = true;
//    options.AppendTrailingSlash = true;
//});

//// Build the app.
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts(); // Enforces HSTS (HTTP Strict Transport Security).
//}

//// Enable middleware for secure HTTP, static files, and routing.
//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseAuthentication();
//app.UseAuthorization();

////app.UseMiddleware<SessionTimeoutMiddleware>();
//app.UseRouting();

//// Map routes using the simplified MapControllerRoute syntax.
//app.MapControllers();

//app.UseSession();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

//app.MapRazorPages();

//app.Run();
