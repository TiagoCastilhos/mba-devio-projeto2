using DevXpert.Store.Core.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace DevXpert.Store.Core.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class ApiConfig
    {
        #region WebApplicationBuilder
        public static WebApplicationBuilder AddApiBehaviorConfig(this WebApplicationBuilder builder)
        {
            builder.Configuration
                   .SetBasePath(builder.Environment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();

            builder.Services
                   .AddControllers()
                   .ConfigureApiBehaviorOptions(options =>
                   {
                       options.SuppressModelStateInvalidFilter = true;
                   });

            return builder;
        }

        public static WebApplicationBuilder AddCorsConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Default", builder => builder.AllowAnyOrigin()
                                                               .AllowAnyMethod()
                                                               .AllowAnyHeader());

                options.AddPolicy("Production", builder => builder.AllowAnyMethod()
                                                                  .WithOrigins("https://kruger.marketplace.com/")
                                                                  .SetIsOriginAllowedToAllowWildcardSubdomains()
                                                                  .AllowAnyHeader()
                                                                  );
            });

            return builder;
        }

        public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
        {
            builder.Services
                   .AddIdentity<IdentityUser, IdentityRole>()
                   .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<AppDbContext>()
                   .AddErrorDescriber<IdentityErrorsConfig>();

            return builder;
        }

          
        #endregion

        #region WebApplication
        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });

            if (!app.Environment.IsDevelopment())
            {
                app.UseCors("Production");
                app.UseHsts();
            }
            else
            {
                app.UseCors("Default");
                app.UseDeveloperExceptionPage();
            }

            app.UseGlobalizationConfig()
               .UseHttpsRedirection()
               //.UseMiddleware<ExceptionMiddleware>()
               //.UseMiddleware<SecurityMiddleware>(app.Environment)
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization();

            return app;
        }

        public static IApplicationBuilder UseEndPointsConfiguration(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }
        #endregion
    }
}
