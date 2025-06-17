using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using XpertStore.Api.Models;

namespace XpertStore.Api.Configuration;

public static class IdentityConfig
{
    public static WebApplicationBuilder AddIdentityConfig(this WebApplicationBuilder builder)
    {
        var JwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
        builder.Services.Configure<JwtSettings>(JwtSettingsSection);

        var jwtSettings = JwtSettingsSection.Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings.Segredo);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audiencia,
                ValidIssuer = jwtSettings.Emissor,
            };
        });

        return builder;
    }
}
