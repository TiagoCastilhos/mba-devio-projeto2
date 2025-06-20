using System.Diagnostics.CodeAnalysis;
using DevXpert.Store.Core.Business.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DevXpert.Store.Core.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class JWTConfig
    {
        public static WebApplicationBuilder AddJWTConfiguration(this WebApplicationBuilder builder)
        {
            var jwtSettings = builder.Configuration.GetSection(JWTSettings.ConfigName).Get<JWTSettings>();

            builder.Services
                   .AddSingleton(jwtSettings);

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
                           IssuerSigningKey = new SymmetricSecurityKey(jwtSettings.ObterChaveEmBytes()),
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidAudiences = jwtSettings.ValidoEm,
                           ValidIssuer = jwtSettings.Emissor,
                           ClockSkew = TimeSpan.Zero
                       };
                   });

            return builder;
        }
    }
}
