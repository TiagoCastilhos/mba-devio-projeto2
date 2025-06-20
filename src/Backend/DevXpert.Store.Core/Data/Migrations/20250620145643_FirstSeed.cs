using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DevXpert.Store.Core.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "2c5e174e-3b0e-446f-86af-483d56fd7210", "Administrator", "ADMINISTRADOR" },
                    { "2", "16aacd76-5c6d-418a-884c-116871ca2fe0", "Vendedor", "VENDEDOR" },
                    { "3", "bd1f5f5b-77e4-4ac3-b101-1f3053f4ee6c", "Cliente", "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f96e5735-7f8a-49a7-8fe1-64304e70257d", 0, "f1aef7e9-db61-4442-a01a-ea58d7609d21", "teste@teste.com", true, true, null, "TESTE@TESTE.COM", "TESTE@TESTE.COM", "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==", null, false, "fdb857cc-1f49-484f-bd6b-bfbba7fedfab", false, "teste@teste.com" });

            migrationBuilder.InsertData(
                table: "CATEGORIAS",
                columns: new[] { "Id", "Ativo", "Descricao", "Nome" },
                values: new object[,]
                {
                    { new Guid("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"), true, "Categoria destinada a vestuário", "Vestuário" },
                    { new Guid("63cb29c3-db97-4744-b01d-def53fc1cccb"), false, "Comidas em geral", "Alimentação" },
                    { new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), true, "Eletrônicos e eletrodomésticos", "Eletrônicos" }
                });

            migrationBuilder.InsertData(
                table: "VENDEDORES",
                columns: new[] { "Id", "Ativo", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"), true, "mail.teste@teste.com", "mail.teste@teste.com", "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2", "f96e5735-7f8a-49a7-8fe1-64304e70257d" });

            migrationBuilder.InsertData(
                table: "PRODUTOS",
                columns: new[] { "Id", "Ativo", "CategoriaId", "Descricao", "Estoque", "Imagem", "Nome", "Preco", "VendedorId" },
                values: new object[,]
                {
                    { new Guid("26361398-ab18-4efd-879f-1f0ad1bb6d9e"), true, new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "teclado mecânico", 15, "00000000-0000-0000-0000-000000000000_imagem.jpg", "Teclado", 100m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("5fa99536-a7c8-403d-a0a0-373f30773054"), true, new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "mouse com fio", 20, "00000000-0000-0000-0000-000000000000_imagem.jpg", "Mouse", 60m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("6fa552cd-bdbf-4f4d-b298-987c3a140275"), false, new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Monitor curso 27", 28, "00000000-0000-0000-0000-000000000000_imagem.jpg", "Monitor", 780m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") },
                    { new Guid("f5dd84d8-ccda-43e8-96cf-be0ccff0de3b"), true, new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"), "Personal Computer", 100, "00000000-0000-0000-0000-000000000000_imagem.jpg", "Computador", 5000m, new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "f96e5735-7f8a-49a7-8fe1-64304e70257d" });

            migrationBuilder.DeleteData(
                table: "CATEGORIAS",
                keyColumn: "Id",
                keyValue: new Guid("2ce8ce71-e766-41ee-839a-f0824f7fd3b8"));

            migrationBuilder.DeleteData(
                table: "CATEGORIAS",
                keyColumn: "Id",
                keyValue: new Guid("63cb29c3-db97-4744-b01d-def53fc1cccb"));

            migrationBuilder.DeleteData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("26361398-ab18-4efd-879f-1f0ad1bb6d9e"));

            migrationBuilder.DeleteData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("5fa99536-a7c8-403d-a0a0-373f30773054"));

            migrationBuilder.DeleteData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("6fa552cd-bdbf-4f4d-b298-987c3a140275"));

            migrationBuilder.DeleteData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("f5dd84d8-ccda-43e8-96cf-be0ccff0de3b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f96e5735-7f8a-49a7-8fe1-64304e70257d");

            migrationBuilder.DeleteData(
                table: "CATEGORIAS",
                keyColumn: "Id",
                keyValue: new Guid("7b87817f-f13c-4a68-87c5-0fc28eda22ce"));

            migrationBuilder.DeleteData(
                table: "VENDEDORES",
                keyColumn: "Id",
                keyValue: new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"));
        }
    }
}
