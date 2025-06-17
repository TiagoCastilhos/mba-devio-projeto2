using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XpertStore.Mvc.data.migrations.sqlite
{
    /// <inheritdoc />
    public partial class UpdateProdutoEstoqueAndAddVendedorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VendedorId",
                table: "Produto",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Vendedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendedor", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_VendedorId",
                table: "Produto",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Vendedor_VendedorId",
                table: "Produto",
                column: "VendedorId",
                principalTable: "Vendedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Vendedor_VendedorId",
                table: "Produto");

            migrationBuilder.DropTable(
                name: "Vendedor");

            migrationBuilder.DropIndex(
                name: "IX_Produto_VendedorId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "VendedorId",
                table: "Produto");
        }
    }
}
