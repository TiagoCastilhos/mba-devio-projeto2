using DevXpert.Store.Core.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiBehaviorConfig()
       .AddDatabase()
       .AddArquivoSettingsConfiguration()
       .AddJWTSettingsConfiguration()
       .AddJWTConfiguration()
       .AddCorsConfig()
       .AddSwaggerConfig()
       .AddIdentityConfig()
       .ResolveDependecies();

var app = builder.Build();

app.MigrateDatabase()
   .UseApiConfiguration()
   .UseSwaggerConfig()
   .UseEndPointsConfiguration();

app.Run();