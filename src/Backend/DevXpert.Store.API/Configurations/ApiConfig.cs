using DevXpert.Store.Core.Application.Configurations;
using DevXpert.Store.Core.Data.Context;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using DevXpert.Store.Core.Business.Models.Settings;
using Microsoft.Extensions.FileProviders;

namespace DevXpert.Store.API.Configurations
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
                                                                  .WithOrigins("https://DevXpertStore.com/")
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
            
            var mvcWwwRootPath = Path.Combine(
                Directory.GetParent(app.Environment.ContentRootPath)!.FullName,
                "DevXpert.Store.MVC/wwwroot/imagens"
            );
            
            if (!Directory.Exists(mvcWwwRootPath))
                Directory.CreateDirectory(mvcWwwRootPath);
            
            var staticFileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(mvcWwwRootPath),
                RequestPath = new PathString("/imagens"),
                
            };

            app.UseGlobalizationConfig()
               .UseHttpsRedirection()
               .UseMiddleware<ExceptionMiddleware>()
               .UseMiddleware<SecurityMiddleware>(app.Environment)
               .UseStaticFiles(staticFileOptions)
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization();

            return app;
        }

        public static WebApplication UseEndPointsConfiguration(this WebApplication app)
        {
            app.MapControllers();

            return app;
        }
        #endregion
    }
}
