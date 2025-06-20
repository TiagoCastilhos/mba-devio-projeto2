using DevXpert.Store.Core.Application.Configurations;
using DevXpert.Store.Core.Data.Seed;

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
   .UseEndPointsConfiguration();

await app.MigrateDatabase();

await app.PopularBancoDeDados();

await app.RunAsync();