using Microsoft.EntityFrameworkCore.Migrations;

namespace DevXpert.Store.Core.data.migrations
{
    public partial class FavoritoMappingAndSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTES_PRODUTOS");

            migrationBuilder.DeleteData(
                table: "CLIENTES",
                keyColumn: "Id",
                keyValue: new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"));

            migrationBuilder.CreateTable(
                name: "FAVORITOS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAVORITOS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAVORITOS_CLIENTES_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "CLIENTES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FAVORITOS_PRODUTOS_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "PRODUTOS",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CLIENTES",
                columns: new[] { "Id", "Ativo", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257c"), true, "cliente@teste.com", "cliente@teste.com", "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==" });

            migrationBuilder.InsertData(
                table: "FAVORITOS",
                columns: new[] { "Id", "ClienteId", "ProdutoId" },
                values: new object[,]
                {
                    { new Guid("099cb44e-44d8-45f2-960c-47139b38bc52"), new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257c"), new Guid("6fa552cd-bdbf-4f4d-b298-987c3a140275") },
                    { new Guid("115a7dde-7803-4836-9799-49046e1d7fb1"), new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257c"), new Guid("5fa99536-a7c8-403d-a0a0-373f30773054") },
                    { new Guid("4f45533c-1f36-46e5-acdb-fbb7e56254ac"), new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257c"), new Guid("26361398-ab18-4efd-879f-1f0ad1bb6d9e") },
                    { new Guid("7f5c5026-518c-4ea2-abe5-8934920d1a27"), new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257c"), new Guid("f5dd84d8-ccda-43e8-96cf-be0ccff0de3b") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FAVORITOS_ProdutoId",
                table: "FAVORITOS",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "UQ_FAVORITO_CLIENTEID_PRODUTOID",
                table: "FAVORITOS",
                columns: new[] { "ClienteId", "ProdutoId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAVORITOS");

            migrationBuilder.DeleteData(
                table: "CLIENTES",
                keyColumn: "Id",
                keyValue: new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257c"));

            migrationBuilder.CreateTable(
                name: "CLIENTES_PRODUTOS",
                columns: table => new
                {
                    ClientesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProdutosId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTES_PRODUTOS", x => new { x.ClientesId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_CLIENTES_PRODUTOS_CLIENTES_ClientesId",
                        column: x => x.ClientesId,
                        principalTable: "CLIENTES",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CLIENTES_PRODUTOS_PRODUTOS_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "PRODUTOS",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CLIENTES",
                columns: new[] { "Id", "Ativo", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("f96e5735-7f8a-49a7-8fe1-64304e70257d"), true, "cliente@teste.com", "cliente@teste.com", "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==" });

            migrationBuilder.CreateIndex(
                name: "IX_CLIENTES_PRODUTOS_ProdutosId",
                table: "CLIENTES_PRODUTOS",
                column: "ProdutosId");
        }
    }
}
