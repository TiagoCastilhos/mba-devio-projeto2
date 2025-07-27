using DevXpert.Store.Core.Application.Configurations;
using DevXpert.Store.Core.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using static DevXpert.Store.MVC.Configurations.CustomIdentityErrorConfig;

namespace DevXpert.Store.MVC.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class MvcConfig
    {
        #region WebApplicationBuilder
        public static WebApplicationBuilder MvcBehaviorConfig(this WebApplicationBuilder builder)
        {
            builder.Configuration
                   .SetBasePath(builder.Environment.ContentRootPath)
                   .AddJsonFile("appsettings.json", true, true)
                   .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                   .AddEnvironmentVariables();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                   .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                   .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
                   .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<AppDbContext>()
                   ;

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                MvcOptionsConfig.ConfigureModelBindingMessages(options.ModelBindingMessageProvider);
            });

            builder.Services.AddAuthorization();

            builder.Services.AddRazorPages();

            return builder;
        }
        #endregion

        #region WebApplication
        public static WebApplication UseMvcConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/erro/500");
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseHsts();
            }

            app.UseGlobalizationConfig()
               .UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            return app;
        }
        #endregion
    }
}
