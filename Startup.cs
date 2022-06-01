using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieMVC
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Example custom middleware using Run() method
            /*app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Middleware 1");
            });*/

            // Example custom middleware using Use() method
            /*app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 1: Incoming Request\n");
                await next.Invoke();
                await context.Response.WriteAsync("Middleware 1: Outgoing Response\n");
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware 2: Incoming Request\n");
                await next.Invoke();
                await context.Response.WriteAsync("Middleware 2: Outgoing Response\n");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Middleware 3: Incoming Request and response generated\n");
            });*/

            // Example custom middleware using Map() method
            // access the url https://localhost:44369/movie and HandleMapMovie method will execute
            app.Map("/movie", HandleMapMovie);

            // access the url https://localhost:44369/contact and HandleMapContect method will execute
            app.Map("/contact", HandleMapContact);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(Configuration["ApplicationName"]);
                });
            });
        }

        private void HandleMapMovie(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Movie Page");
            });
        }

        private void HandleMapContact(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Contact Page");
            });
        }
    }
}
