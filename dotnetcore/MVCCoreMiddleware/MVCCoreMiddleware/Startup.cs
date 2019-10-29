using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace MVCCoreMiddleware
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //ASPNETCORE
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Map("/about", (appContext) =>
             {
                 appContext.Use(async (context, next) =>
                 {
                     await context.Response.WriteAsync("< br /> From About Middleware 1 request");
                     await next.Invoke();
                     await context.Response.WriteAsync("<br/> From About Middleware 1 response");
                 });
                 appContext.Run(async (context) =>
                 {
                     await context.Response.WriteAsync("<br/>About page!");
                 });
             });
            app.Map("/contact", (appContext) =>
            {
                appContext.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("< br /> From contact Middleware 1 request");
                    await next.Invoke();
                    await context.Response.WriteAsync("<br/> From contact Middleware 1 response");
                });
                appContext.Run(async (context) =>
                {
                    await context.Response.WriteAsync("<br/>About page!");
                });
            });
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Type","text/plain");
                await next.Invoke();                
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<br/> From Middleware 1 request");
                await next.Invoke();
                await context.Response.WriteAsync("<br/> From Middleware 1 response");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<br/> From Middleware 2 request");
                await next.Invoke();
                await context.Response.WriteAsync("<br/> From Middleware 2 response");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<br/> From Middleware 3 request");
                await next.Invoke();
                await context.Response.WriteAsync("<br/> From Middleware 3 response");
            });
            ////app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("<br/>Hello World!");
            //});

            app.MapWhen((context) => context.Request.Query["lang"] == "en", (appContext) =>
                 {
                     appContext.Run(async (httpcontext) =>
                     {
                         await httpcontext.Response.WriteAsync("<h2>English home page</h2>");
     
                     });
                 });
            app.MapWhen((context) => context.Request.Query["lang"] == "hi", (appContext) =>
            {
                appContext.Run(async (httpcontext) =>
                {
                    await httpcontext.Response.WriteAsync("<h2>Hindi home page</h2>");

                });
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<h2>Default page</h2>");
            });
        }
    }
}
