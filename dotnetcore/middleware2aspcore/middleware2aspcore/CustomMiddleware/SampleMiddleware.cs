using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace middleware2aspcore.CustomMiddleware
{
    public class SampleMiddleware
    {
        private RequestDelegate _next;

        public SampleMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<br/> From Sample middleware - request");
            await _next.Invoke(context);
            await context.Response.WriteAsync("<br/> From Sample middleware - response");
        }
    }

    public static class MiddlewareExtensions
    {
        public static void useSample(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SampleMiddleware>();
        }
    }
}
