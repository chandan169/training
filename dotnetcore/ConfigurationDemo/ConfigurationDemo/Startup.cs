using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigurationDemo
{
    public class Startup
    {
        public static Dictionary<string, string> configData = new Dictionary<string, string>();


            public Startup()
        {
            configData.Add("Projector", "epson");
            configData.Add("Laptop", "HP");

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddInMemoryCollection(configData)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("mysettings.json", optional: true)
                .AddXmlFile("mysettings.xml", optional: true)
                .AddEnvironmentVariables(".ASPNETCORE_");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var data = Configuration.GetSection("ack");
            var arch = Configuration.GetValue<string>("PROCESSOR_ARCHITECTURE");
            services.Configure<AppConfiguration>(Configuration);
            services.Configure<ProjectDetails>(Configuration.GetSection("ProjectDetail"));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
