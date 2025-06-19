using DevXpert.Store.Core.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.MvcBehaviorConfig()
       .AddDatabase()
       .AddArquivoSettingsConfiguration()
       .ResolveDependecies();

var app = builder.Build();

app.UseMvcConfiguration()
   .MigrateDatabase().Wait();

app.Run();