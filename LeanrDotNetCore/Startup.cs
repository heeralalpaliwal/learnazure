using LeanrDotNetCore.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeanrDotNetCore
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            //services.AddMvc();
            services.AddSingleton<IConfiguratinService, ConfiguratinService>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async context => {
                await context.Response.WriteAsync("Hello world");
            });


            //app.Use(async (context, next) =>
            //    {
            //        //await context.Response.WriteAsync("middleware 1");
            //        await next.Invoke();
            //    });

            //app.UseMiddleware<LogURLMiddleware>();
            //app.UseMvc();

            //app.UseMvcWithDefaultRoute();
            //app.UseRouting();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            //});
        }

        async Task TerminalMiddleWare(HttpContext context)
        {
            
            await context.Response.WriteAsync("hello world");
           // await next.Invoke();
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            //Home/Index 
            routeBuilder.MapRoute("Default", "{controller = Home}/{action = Index}/{id?}");
        }



    }

    public class LogURLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogURLMiddleware> _logger;
        public LogURLMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<LogURLMiddleware>() ??
            throw new ArgumentNullException(nameof(loggerFactory));
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Request URL: {Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");
            await this._next(context);
        }
    }
}
