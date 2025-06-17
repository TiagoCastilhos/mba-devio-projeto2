using XpertStore.Api.Configuration;
using XpertStore.Data.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder
    .AddApiConfig()
    .AddCorsConfig()
    .AddSwaggerConfig()
    .AddDatabaseSelector()
    .AddIdentityConfig();

builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Development");
}
else
{
    app.UseCors("Production");
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseDbMigrationHelper();

app.Run();
