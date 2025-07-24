using DevXpert.Store.Core.Business.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DevXpert.Store.API.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class JWTConfig
    {
        public static WebApplicationBuilder AddJWTConfiguration(this WebApplicationBuilder builder)
        {
            var appSettings = builder.Configuration.GetSection(JWTSettings.ConfigName).Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Jwt);

            builder.Services
                   .AddAuthentication(t =>
                   {
                       t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                       t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   })
                   .AddJwtBearer(t =>
                   {
                       t.RequireHttpsMetadata = true;
                       t.SaveToken = true;
                       t.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(key),
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidAudiences = appSettings.ValidoEm,
                           ValidIssuer = appSettings.Emissor,
                           ClockSkew = TimeSpan.Zero
                       };
                   });

            return builder;
        }
    }
}
