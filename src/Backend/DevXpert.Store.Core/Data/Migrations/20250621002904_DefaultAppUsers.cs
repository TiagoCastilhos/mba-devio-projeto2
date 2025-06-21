using Microsoft.EntityFrameworkCore.Migrations;

namespace DevXpert.Store.Core.data.migrations
{
    public partial class DefaultAppUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "f96e5735-7f8a-49a7-8fe1-64304e70257b", 0, "f1aef7e9-db61-4442-a01a-ea58d7609d21", "admin@teste.com", true, true, null, "ADMIN@TESTE.COM", "ADMIN@TESTE.COM", "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==", null, false, "fdb857cc-1f49-484f-bd6b-bfbba7fedfab", false, "admin@teste.com" },
                    { "f96e5735-7f8a-49a7-8fe1-64304e70257c", 0, "f1aef7e9-db61-4442-a01a-ea58d7609d21", "cliente@teste.com", true, true, null, "CLIENTE@TESTE.COM", "CLIENTE@TESTE.COM", "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==", null, false, "fdb857cc-1f49-484f-bd6b-bfbba7fedfab", false, "cliente@teste.com" }
                });

            migrationBuilder.InsertData(
                table: "CLIENTES",
                columns: new[] { "Id", "Ativo", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"), true, "cliente@teste.com", "cliente@teste.com", "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "f96e5735-7f8a-49a7-8fe1-64304e70257b" },
                    { "3", "f96e5735-7f8a-49a7-8fe1-64304e70257c" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "f96e5735-7f8a-49a7-8fe1-64304e70257b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "f96e5735-7f8a-49a7-8fe1-64304e70257c" });

            migrationBuilder.DeleteData(
                table: "CLIENTES",
                keyColumn: "Id",
                keyValue: new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f96e5735-7f8a-49a7-8fe1-64304e70257b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f96e5735-7f8a-49a7-8fe1-64304e70257c");
        }
    }
}
