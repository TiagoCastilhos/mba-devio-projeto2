using Microsoft.AspNetCore.Identity;
using XpertStore.Data.Data;

namespace XpertStore.Mvc.Configuration;

public static class DatabaseSelectorExtension
{
    public static WebApplicationBuilder AddDatabaseSelector(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<ApplicationDbContext, SqliteDbContext>();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SqliteDbContext>();
        }
        else
        {
            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
        return builder;
    }
}
