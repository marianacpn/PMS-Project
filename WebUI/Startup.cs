using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application;
using Infrastructure;
using Persistence;
using Application.Common.Interfaces;
using WebUI.Services;
using WebUI.Filters;
using FluentValidation.AspNetCore;
using System.Reflection;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        private IServiceCollection _services;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication(Environment, Configuration);
            services.AddInfrastructure();
            services.AddPersistence(Configuration);


            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IHostUrlService, HostUrlService>();
            services.AddScoped<IApplicationVersionService, ApplicationVersionService>();


            services.AddHttpContextAccessor();
            services.AddSession();


            services.AddControllersWithViews(config =>
            {
                config.Filters.Add<SessionExpireFilter>();
            })
                    .AddRazorRuntimeCompilation()
                    .AddNewtonsoftJson()
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                        fv.RegisterValidatorsFromAssemblyContaining<ICurrentUserService>();
                    });


            _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHealthChecks("/health");

            app.UseSession();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
