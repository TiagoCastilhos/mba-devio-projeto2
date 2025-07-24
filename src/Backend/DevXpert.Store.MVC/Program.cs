using DevXpert.Store.Core.Application.Configurations;
using DevXpert.Store.MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddArquivoSettingsConfiguration()
       .AddDatabase()
       .MvcBehaviorConfig()
       .ResolveDependecies();

var app = builder.Build();

app.UseMvcConfiguration()
   .MigrateDatabase().Wait();

app.Run();