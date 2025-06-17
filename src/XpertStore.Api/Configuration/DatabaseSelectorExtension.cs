using Microsoft.AspNetCore.Identity;
using XpertStore.Data.Data;

namespace XpertStore.Api.Configuration;

public static class DatabaseSelectorExtension
{
    public static WebApplicationBuilder AddDatabaseSelector(this WebApplicationBuilder builder)
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddDbContext<ApplicationDbContext, SqliteDbContext>();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SqliteDbContext>();

        }
        else
        {
            builder.Services.AddDbContext<ApplicationDbContext>();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
        return builder;
    }
}
