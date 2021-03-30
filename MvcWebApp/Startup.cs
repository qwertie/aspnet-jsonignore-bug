using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebApp
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
            services.AddControllersWithViews();

            services.AddLogging((ILoggingBuilder builder) => {
                builder.AddConsole();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                //options.InvalidModelStateResponseFactory = (ActionContext context) =>
                //{
                //    var problemDetails = CreateValidationProblemDetails(context.HttpContext, context.ModelState);
                //    ObjectResult result;
                //    result = new BadRequestObjectResult(problemDetails);
                //    result.ContentTypes.Add("application/problem+json");
                //    result.ContentTypes.Add("application/problem+xml");

                //    return result;
                //};

                //ValidationProblemDetails CreateValidationProblemDetails(
                //    Microsoft.AspNetCore.Http.HttpContext httpContext,
                //    Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelStateDictionary)
                //{
                //    var problemDetails = new ValidationProblemDetails(modelStateDictionary) { Status = 400 };

                //    if (options.ClientErrorMapping.TryGetValue(400, out var clientErrorData))
                //    {
                //        problemDetails.Title ??= clientErrorData.Title;
                //        problemDetails.Type ??= clientErrorData.Link;
                //    }

                //    return problemDetails;
                //}
            });

            // In ASP.NET Core apps, a Scope is created around each server request.
            // So AddScoped<X, Y>() will recreate class Y for each HTTP request.
            services.AddScoped<DbContext, ExampleDbContext>();
            services.AddScoped<ExampleDbContext, ExampleDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var context = new ExampleDbContext(loggerFactory, Configuration.GetSection("ConnectionStrings"))) {
                context.Database.Migrate();
            }
        }
    }
}
