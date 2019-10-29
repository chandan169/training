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
using Microsoft.Extensions.FileProviders;

namespace BuiltInMiddleware
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
            app.UseHttpsRedirection();

            var option = new DefaultFilesOptions();
            option.DefaultFileNames.Clear();
            option.DefaultFileNames.Add("hello.html");
            app.UseDefaultFiles(option);

            app.UseFileServer(new FileServerOptions()
            {
                RequestPath = "/files",
                EnableDirectoryBrowsing = true,
                FileProvider =new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "files"))                
            });
            //var fileOption = new DefaultFilesOptions();
            //fileOption.RequestPath = "/files";
            //fileOption.FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "files"));
            //app.UseDefaultFiles(fileOption);
            app.UseDefaultFiles(); //serving from wwwroot
            //app.UseDefaultFiles(/*params*/); //http://localhost:1234/files/ -> serve index.html
            app.UseStaticFiles(new StaticFileOptions()
            {
                RequestPath = "/files",
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "files"))
            });
            app.UseStaticFiles(); //for wwwroot
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    RequestPath = "/files",
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "files"))
            //});

            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "ProductRoute",
                //    template: "Products/{action=Index}/{id?}"
                //    );
                //app.UseMvcWithDefaultRoute();
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
