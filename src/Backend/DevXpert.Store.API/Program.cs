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

app.UseApiConfiguration()
   .UseSwaggerConfig()
   .UseEndPointsConfiguration()
   .MigrateDatabase().Wait();

app.Run();