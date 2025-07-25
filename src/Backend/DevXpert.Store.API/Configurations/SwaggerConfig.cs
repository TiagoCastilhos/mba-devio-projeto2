﻿using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace DevXpert.Store.API.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfig
    {
        public static WebApplicationBuilder AddSwaggerConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();

            builder.Services
                   .AddSwaggerGen(static c =>
                   {
                       c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                       {
                           Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                           Name = "Authorization",
                           Scheme = "Bearer",
                           BearerFormat = "JWT",
                           In = ParameterLocation.Header,
                           Type = SecuritySchemeType.ApiKey
                       });

                       c.AddSecurityRequirement(new OpenApiSecurityRequirement
                       {
                           {
                               new OpenApiSecurityScheme
                               {
                                   Reference = new OpenApiReference
                                   {
                                       Type = ReferenceType.SecurityScheme,
                                       Id = "Bearer"
                                   }
                               },
                               Array.Empty<string>()
                           }
                       });
                   });

            return builder;
        }

        public static WebApplication UseSwaggerConfig(this WebApplication app)
        {
            app.UseSwagger()
               .UseSwaggerUI();

            return app;
        }
    }
}
