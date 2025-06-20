using DevXpert.Store.Core.Business.Constants;
using DevXpert.Store.Core.Business.Models;
using DevXpert.Store.Core.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevXpert.Store.Core.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static async Task PopularBancoDeDados(this WebApplication application)
        {
            using var scope = application.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<AppDbContext>();

            if (await dbContext.Users.AnyAsync())
                return;

            var vendedorId = Guid.Parse("f96e5735-7f8a-49a7-8fe1-64304e70257d");
            var adminId = Guid.Parse("2BF7E83D-265F-4A05-8EDB-2060C24743BC");
            var senhaVendedor = new PasswordHasher<IdentityUser>().HashPassword(null, "@Aa12345");
            var senhaAdmin = new PasswordHasher<IdentityUser>().HashPassword(null, "@Bb12345");

            await dbContext.Roles.AddRangeAsync(
                new IdentityRole { Id = "1", Name = RoleConstants.Administrador, NormalizedName = RoleConstants.Administrador.ToUpper(), ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210" },
                new IdentityRole { Id = "2", Name = RoleConstants.Vendedor, NormalizedName = RoleConstants.Vendedor.ToUpper(), ConcurrencyStamp = "16aacd76-5c6d-418a-884c-116871ca2fe0" },
                new IdentityRole { Id = "3", Name = RoleConstants.Cliente, NormalizedName = RoleConstants.Cliente.ToUpper(), ConcurrencyStamp = "bd1f5f5b-77e4-4ac3-b101-1f3053f4ee6c" });

            await dbContext.Users.AddRangeAsync(
                new IdentityUser
                {
                    Id = vendedorId.ToString(),
                    UserName = "teste@teste.com",
                    NormalizedUserName = "TESTE@TESTE.COM",
                    NormalizedEmail = "TESTE@TESTE.COM",
                    Email = "teste@teste.com",
                    PasswordHash = senhaVendedor,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    ConcurrencyStamp = "D95B4C73-505E-412A-ACC2-51ADB3032456",
                    SecurityStamp = "A0D52FDC-BF02-49F7-BE25-D903000134D6"
                },
                new IdentityUser
                {
                    Id = adminId.ToString(),
                    UserName = "admin@admin.com",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    Email = "admin@admin.com",
                    PasswordHash = senhaAdmin,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    ConcurrencyStamp = "4CED56D2-8299-479D-B4BE-AFA7F45E2123",
                    SecurityStamp = "72A4E97C-BF28-44BF-A57A-F7A8748B9349"
                }
            );

            await dbContext.UserRoles.AddRangeAsync(
                new IdentityUserRole<string> { UserId = vendedorId.ToString(), RoleId = "2" },
                new IdentityUserRole<string> { UserId = adminId.ToString(), RoleId = "1" }
            );

            await dbContext.Vendedores.AddAsync(new Vendedor(vendedorId, "mail.teste@teste.com", "mail.teste@teste.com", senhaVendedor));

            await dbContext.Categorias.AddRangeAsync(
                new Categoria(Guid.Parse("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"), "Vestuário", "Categoria destinada a vestuário"),
                new Categoria(Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Eletrônicos", "Eletrônicos e eletrodomésticos"),
                new Categoria(Guid.Parse("63cb29c3-db97-4744-b01d-def53fc1cccb"), "Alimentação", "Comidas em geral", false)
            );

            await dbContext.Produtos.AddRangeAsync(
                new Produto(Guid.Parse("39B3EE14-199E-43FE-8FBD-A3BB7D6B7C07"), 100, 5000, "Computador", "Personal Computer", "00000000-0000-0000-0000-000000000000_imagem.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.Parse("C9CAA43C-9DA4-481E-9C43-87F01E7AB746"), 20, 60, "Mouse", "mouse com fio", "00000000-0000-0000-0000-000000000000_imagem.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.Parse("13FC49E9-33C8-497E-8590-E6B8BAB439BF"), 15, 100, "Teclado", "teclado mecânico", "00000000-0000-0000-0000-000000000000_imagem.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.Parse("3BBC165E-133F-4E15-B966-C30A2AECA744"), 28, 780, "Monitor", "Monitor curso 27", "00000000-0000-0000-0000-000000000000_imagem.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId, false)
            );

            await dbContext.SaveChangesAsync();
        }
    }
}