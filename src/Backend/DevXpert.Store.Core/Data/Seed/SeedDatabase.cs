using DevXpert.Store.Core.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Seed
{
    //TODO: MAPEAR A ROLE/CLAIM DO ADMIN
    public static class SeedDatabase
    {
        public static void Seed(ModelBuilder builder)
        {
            var vendedorId = Guid.Parse("f96e5735-7f8a-49a7-8fe1-64304e70257d");
            var senha = new PasswordHasher<IdentityUser>().HashPassword(null, "@Aa12345");

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210" },
                new IdentityRole { Id = "2", Name = "Vendedor", NormalizedName = "VENDEDOR", ConcurrencyStamp = "16aacd76-5c6d-418a-884c-116871ca2fe0" },
                new IdentityRole { Id = "3", Name = "Cliente", NormalizedName = "CLIENTE", ConcurrencyStamp = "bd1f5f5b-77e4-4ac3-b101-1f3053f4ee6c" }
            );

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = vendedorId.ToString(),
                    UserName = "teste@teste.com",
                    NormalizedUserName = "TESTE@TESTE.COM",
                    NormalizedEmail = "TESTE@TESTE.COM",
                    Email = "teste@teste.com",
                    PasswordHash = senha,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = vendedorId.ToString(), RoleId = "2" }
            );

            builder.Entity<Vendedor>().HasData(
                new Vendedor(vendedorId, "mail.teste@teste.com", "mail.teste@teste.com", senha)
            );

            builder.Entity<Categoria>().HasData(
                new Categoria(Guid.Parse("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"), "Vestuário", "Categoria destinada a vestuário"),
                new Categoria(Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Eletrônicos", "Eletrônicos e eletrodomésticos"),
                new Categoria(Guid.Parse("63cb29c3-db97-4744-b01d-def53fc1cccb"), "Alimentação", "Comidas em geral", false)
            );

            builder.Entity<Produto>().HasData(
                new Produto(Guid.NewGuid(), 100, 5000, "Computador", "Personal Computer", "00000000-0000-0000-0000-000000000000_imagem.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.NewGuid(), 20, 60, "Mouse", "mouse com fio", "00000000-0000-0000-0000-000000000000_imagem.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.NewGuid(), 15, 100, "Teclado", "teclado mecânico", "00000000-0000-0000-0000-000000000000_imagem.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.NewGuid(), 28, 780, "Monitor", "Monitor curso 27", "00000000-0000-0000-0000-000000000000_imagem.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId, false)
            );
        }
    }
}