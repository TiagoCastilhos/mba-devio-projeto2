using DevXpert.Store.Core.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Store.Core.Data.Seed
{
    public static class SeedDatabase
    {
        public static void Seed(ModelBuilder builder)
        {
            var adminId = Guid.Parse("f96e5735-7f8a-49a7-8fe1-64304e70257b");
            var clienteId = Guid.Parse("f96e5735-7f8a-49a7-8fe1-64304e70257c");
            var vendedorId = Guid.Parse("f96e5735-7f8a-49a7-8fe1-64304e70257d");

            var senha = "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==";//@Aa12345

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRADOR", ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210" },
                new IdentityRole { Id = "2", Name = "Vendedor", NormalizedName = "VENDEDOR", ConcurrencyStamp = "16aacd76-5c6d-418a-884c-116871ca2fe0" },
                new IdentityRole { Id = "3", Name = "Cliente", NormalizedName = "CLIENTE", ConcurrencyStamp = "bd1f5f5b-77e4-4ac3-b101-1f3053f4ee6c" }
            );

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = vendedorId.ToString(),
                    UserName = "vendedor@teste.com",
                    NormalizedUserName = "VENDEDOR@TESTE.COM",
                    NormalizedEmail = "VENDEDOR@TESTE.COM",
                    Email = "vendedor@teste.com",
                    PasswordHash = senha,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    ConcurrencyStamp = "f1aef7e9-db61-4442-a01a-ea58d7609d21",
                    SecurityStamp = "fdb857cc-1f49-484f-bd6b-bfbba7fedfab"
                },
                new IdentityUser
                {
                    Id = clienteId.ToString(),
                    UserName = "cliente@teste.com",
                    NormalizedUserName = "CLIENTE@TESTE.COM",
                    NormalizedEmail = "CLIENTE@TESTE.COM",
                    Email = "cliente@teste.com",
                    PasswordHash = senha,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    ConcurrencyStamp = "f1aef7e9-db61-4442-a01a-ea58d7609d21",
                    SecurityStamp = "fdb857cc-1f49-484f-bd6b-bfbba7fedfab"
                },
                new IdentityUser
                {
                    Id = adminId.ToString(),
                    UserName = "admin@teste.com",
                    NormalizedUserName = "ADMIN@TESTE.COM",
                    NormalizedEmail = "ADMIN@TESTE.COM",
                    Email = "admin@teste.com",
                    PasswordHash = senha,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    ConcurrencyStamp = "f1aef7e9-db61-4442-a01a-ea58d7609d21",
                    SecurityStamp = "fdb857cc-1f49-484f-bd6b-bfbba7fedfab"
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminId.ToString(), RoleId = "1" },
                new IdentityUserRole<string> { UserId = vendedorId.ToString(), RoleId = "2" },
                new IdentityUserRole<string> { UserId = clienteId.ToString(), RoleId = "3" }
            );

            builder.Entity<Vendedor>().HasData(
                new Vendedor(vendedorId, "vendedor@teste.com", "vendedor@teste.com")
            );

            builder.Entity<Cliente>().HasData(
                new Cliente(clienteId, "cliente@teste.com", "cliente@teste.com")
            );

            builder.Entity<Categoria>().HasData(
                new Categoria(Guid.Parse("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"), "Vestuário", "Categoria destinada a vestuário"),
                new Categoria(Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Eletrônicos", "Eletrônicos e eletrodomésticos"),
                new Categoria(Guid.Parse("63cb29c3-db97-4744-b01d-def53fc1cccb"), "Alimentação", "Comidas em geral", false)
            );

            builder.Entity<Produto>().HasData(
                new Produto(Guid.Parse("f5dd84d8-ccda-43e8-96cf-be0ccff0de3b"), 100, 5000, "Computador", "Personal Computer", "f5dd84d8-ccda-43e8-96cf-be0ccff0de3b_computador.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.Parse("5fa99536-a7c8-403d-a0a0-373f30773054"), 20, 60, "Mouse", "mouse com fio", "5fa99536-a7c8-403d-a0a0-373f30773054_mouse.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.Parse("26361398-ab18-4efd-879f-1f0ad1bb6d9e"), 15, 100, "Teclado", "teclado mecânico", "26361398-ab18-4efd-879f-1f0ad1bb6d9e_teclado.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId),
                new Produto(Guid.Parse("6fa552cd-bdbf-4f4d-b298-987c3a140275"), 28, 780, "Monitor", "Monitor curso 27", "6fa552cd-bdbf-4f4d-b298-987c3a140275_monitor_27.jpg", Guid.Parse("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), vendedorId, false)
            );

            builder.Entity<Favorito>().HasData(
                new Favorito(Guid.Parse("7f5c5026-518c-4ea2-abe5-8934920d1a27"), clienteId, Guid.Parse("f5dd84d8-ccda-43e8-96cf-be0ccff0de3b")),
                new Favorito(Guid.Parse("115a7dde-7803-4836-9799-49046e1d7fb1"), clienteId, Guid.Parse("5fa99536-a7c8-403d-a0a0-373f30773054")),
                new Favorito(Guid.Parse("4f45533c-1f36-46e5-acdb-fbb7e56254ac"), clienteId, Guid.Parse("26361398-ab18-4efd-879f-1f0ad1bb6d9e")),
                new Favorito(Guid.Parse("099cb44e-44d8-45f2-960c-47139b38bc52"), clienteId, Guid.Parse("6fa552cd-bdbf-4f4d-b298-987c3a140275"))
            );
        }
    }
}