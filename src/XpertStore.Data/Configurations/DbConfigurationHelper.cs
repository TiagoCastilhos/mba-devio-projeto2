using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XpertStore.Data.Data;
using XpertStore.Data.Models;

namespace XpertStore.Data.Configurations;

public static class DbMigrationHelperExtension
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        DbMigrationHelpers.EnsureSeedData(app).Wait();
    }
}

public static class DbMigrationHelpers
{

    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }

    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var contextId = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (env.IsDevelopment())
        {
            await contextId.Database.MigrateAsync();
        }

        await EnsureSeedProducts(contextId);
    }

    public static async Task EnsureSeedProducts(ApplicationDbContext contextId)
    {
        if (contextId.Users.Any())
            return;
        var userId = Guid.NewGuid();
        await contextId.Users.AddAsync(new IdentityUser
        {
            Id = userId.ToString(),
            UserName = "teste@teste.com",
            NormalizedUserName = "TESTE@TESTE.COM",
            Email = "teste@teste.com",
            NormalizedEmail = "TESTE@TESTE.COM",
            AccessFailedCount = 0,
            LockoutEnabled = false,
            PasswordHash = "AQAAAAIAAYagAAAAEEdWhqiCwW/jZz0hEM7aNjok7IxniahnxKxxO5zsx2TvWs4ht1FUDnYofR8JKsA5UA==",
            TwoFactorEnabled = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        });

        Vendedor vendedor1 = new Vendedor
        {
            Id = userId
        };
        await contextId.AddAsync(vendedor1);

        var categoria1 = new Categoria
        {
            Id = Guid.NewGuid(),
            Nome = "Categoria 1",
            Descricao = "Descricao Categoria 1"
        };

        await contextId.AddRangeAsync([
            categoria1,
            new Categoria {
                Id = Guid.NewGuid(),
                Nome = "Categoria 2",
                Descricao = "Descricao Categoria 2"
            }
        ]);

        await contextId.AddAsync(new Produto
        {
            Id = Guid.NewGuid(),
            Nome = "Nome Teste",
            Descricao = "Descricao Teste",
            Imagem = "82358ae7-f319-4852-b603-7abb66596d04_project2.jpg",
            Preco = 456,
            Estoque = 123,
            Categoria = categoria1,
            Vendedor = vendedor1,
        });

        await contextId.SaveChangesAsync();
    }
}
