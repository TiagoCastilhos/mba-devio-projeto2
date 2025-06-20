using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DevXpert.Store.Core.Data.Context;

namespace DevXpert.Store.Core.Application.Configurations
{
    public static class DatabaseConfig
    {
        public static WebApplicationBuilder AddDatabase(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            var configuration = builder.Configuration;

            if (env.IsDevelopment())
                builder.Services
                       .AddDbContext<AppDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DefaultConnectionLite"),
                                                                                opt => opt.CommandTimeout(45)
                                                                                          .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
            else
                builder.Services
                       .AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                                                                   opt => opt.CommandTimeout(45)
                                                                                             .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                                                                                             .UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));
            return builder;
        }

        public static WebApplication MigrateDatabase(this WebApplication app)
        {
            using var appContext = GetDbContext(app);
            appContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(5));

            appContext.Database.Migrate();

            return app;
        }

        private static AppDbContext GetDbContext(this IApplicationBuilder app)
        {
            return app.ApplicationServices
                      .GetRequiredService<IServiceScopeFactory>()
                      .CreateScope()
                      .ServiceProvider
                      .GetService<AppDbContext>();
        }
    }
}
