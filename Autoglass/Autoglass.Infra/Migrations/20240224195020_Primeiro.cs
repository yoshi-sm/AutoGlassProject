using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autoglass.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Primeiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id_Fornecedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id_Fornecedor);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id_Produto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fl_Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Dt_Fabricacao = table.Column<DateOnly>(type: "date", nullable: true),
                    Dt_Validade = table.Column<DateOnly>(type: "date", nullable: true),
                    Id_Fornecedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id_Produto);
                    table.ForeignKey(
                        name: "FK_Produto_Fornecedor_Id_Fornecedor",
                        column: x => x.Id_Fornecedor,
                        principalTable: "Fornecedor",
                        principalColumn: "Id_Fornecedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Id_Fornecedor",
                table: "Produto",
                column: "Id_Fornecedor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Fornecedor");
        }
    }
}
