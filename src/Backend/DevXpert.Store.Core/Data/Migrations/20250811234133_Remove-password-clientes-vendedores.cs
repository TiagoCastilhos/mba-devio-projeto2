using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevXpert.Store.Core.data.migrations
{
    /// <inheritdoc />
    public partial class Removepasswordclientesvendedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "VENDEDORES");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "CLIENTES");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "VENDEDORES",
                type: "VARCHAR(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "CLIENTES",
                type: "VARCHAR(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CLIENTES",
                keyColumn: "Id",
                keyValue: new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257c"),
                column: "Senha",
                value: "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==");

            migrationBuilder.UpdateData(
                table: "VENDEDORES",
                keyColumn: "Id",
                keyValue: new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"),
                column: "Senha",
                value: "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==");
        }
    }
}
