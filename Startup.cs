using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MovieMVC.Data;
using MovieMVC.Repositories;
using MovieMVC.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using MovieMVC.Models;

namespace MovieMVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // use MVC template
            services.AddControllersWithViews();
            // Connect to sql express
            services.AddDbContext<MovieContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MovieContext>();

            ConfigurationIdentity(services);

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            logger.AddFile("Logs/log.txt");
            
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseStatusCodePagesWithRedirects("/error/{0}");
                app.UseExceptionHandler("/Home/Error");
                /*app.Use(async (context, next) =>
                {
                    await next.Invoke();
                    string errorMessage = "";
                    switch (context.Response.StatusCode)
                    {
                        case StatusCodes.Status404NotFound:
                            errorMessage = "Error 404. Page Not Found";
                            break;
                        case StatusCodes.Status500InternalServerError:
                            errorMessage = "Error 500. General Error";
                            break;
                    }

                    await context.Response.WriteAsync(errorMessage);                    
                });*/
            }

            app.UseHttpsRedirection();

            // Adding static files middleware to serve the static files in wwwroot folder
            app.UseStaticFiles();

            // Adding static files middleware to serve the static files in resources folder
            // so we can access all static files in resources folder from browser
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Resources")),
                RequestPath = "/files"
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // for convention based routing and attribute routing
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // for attribute routing only
                //endpoints.MapControllers();
            });
        }

        private void ConfigurationIdentity(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                // User Settings
                options.User.RequireUniqueEmail = true;

                // Password Settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }
    }
}
