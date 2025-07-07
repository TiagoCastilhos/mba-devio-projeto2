using DevXpert.Store.Core.Business.Models.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace DevXpert.Store.Core.Application.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SettingsConfig
    {
        public static WebApplicationBuilder AddJWTSettingsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JWTSettings>(
                builder.Configuration.GetSection(JWTSettings.ConfigName)
            );

            return builder;
        }

        public static WebApplicationBuilder AddArquivoSettingsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ArquivoSettings>(
               builder.Configuration.GetSection(ArquivoSettings.ConfigName)
           );

            return builder;
        }

        public static WebApplicationBuilder AddAppCredentialsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AppCredentialsSettings>(
               builder.Configuration.GetSection(AppCredentialsSettings.ConfigName)
           );

            return builder;
        }
    }
}
