using Microsoft.EntityFrameworkCore.Migrations;

namespace DevXpert.Store.Core.data.migrations
{
    public partial class Unique_produto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UQ_PRODUTO_NOME_VENDEDORID",
                table: "PRODUTOS",
                columns: new[] { "Nome", "VendedorId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_PRODUTO_NOME_VENDEDORID",
                table: "PRODUTOS");
        }
    }
}
