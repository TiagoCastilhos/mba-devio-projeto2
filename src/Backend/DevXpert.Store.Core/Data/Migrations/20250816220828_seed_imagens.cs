using Microsoft.EntityFrameworkCore.Migrations;

namespace DevXpert.Store.Core.data.migrations
{
    public partial class seed_imagens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("26361398-ab18-4efd-879f-1f0ad1bb6d9e"),
                column: "Imagem",
                value: "26361398-ab18-4efd-879f-1f0ad1bb6d9e_teclado.jpg");

            migrationBuilder.UpdateData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("5fa99536-a7c8-403d-a0a0-373f30773054"),
                column: "Imagem",
                value: "5fa99536-a7c8-403d-a0a0-373f30773054_mouse.jpg");

            migrationBuilder.UpdateData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("6fa552cd-bdbf-4f4d-b298-987c3a140275"),
                column: "Imagem",
                value: "6fa552cd-bdbf-4f4d-b298-987c3a140275_monitor_27.jpg");

            migrationBuilder.UpdateData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("f5dd84d8-ccda-43e8-96cf-be0ccff0de3b"),
                column: "Imagem",
                value: "f5dd84d8-ccda-43e8-96cf-be0ccff0de3b_computador.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("26361398-ab18-4efd-879f-1f0ad1bb6d9e"),
                column: "Imagem",
                value: "00000000-0000-0000-0000-000000000000_imagem.jpg");

            migrationBuilder.UpdateData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("5fa99536-a7c8-403d-a0a0-373f30773054"),
                column: "Imagem",
                value: "00000000-0000-0000-0000-000000000000_imagem.jpg");

            migrationBuilder.UpdateData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("6fa552cd-bdbf-4f4d-b298-987c3a140275"),
                column: "Imagem",
                value: "00000000-0000-0000-0000-000000000000_imagem.jpg");

            migrationBuilder.UpdateData(
                table: "PRODUTOS",
                keyColumn: "Id",
                keyValue: new Guid("f5dd84d8-ccda-43e8-96cf-be0ccff0de3b"),
                column: "Imagem",
                value: "00000000-0000-0000-0000-000000000000_imagem.jpg");
        }
    }
}
