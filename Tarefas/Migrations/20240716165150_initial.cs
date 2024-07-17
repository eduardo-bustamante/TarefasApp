using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tarefas.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Condicoes",
                columns: table => new
                {
                    CondicaoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condicoes", x => x.CondicaoId);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDeVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CondicaoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarefas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarefas_Condicoes_CondicaoId",
                        column: x => x.CondicaoId,
                        principalTable: "Condicoes",
                        principalColumn: "CondicaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Nome" },
                values: new object[,]
                {
                    { "casa", "Casa" },
                    { "compra", "Compra" },
                    { "contato", "Contato" },
                    { "ex", "Exercicio" },
                    { "trabalho", "Trabalho" }
                });

            migrationBuilder.InsertData(
                table: "Condicoes",
                columns: new[] { "CondicaoId", "Nome" },
                values: new object[,]
                {
                    { "aberto", "Aberto" },
                    { "concluido", "Concluido" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_CategoriaId",
                table: "Tarefas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_CondicaoId",
                table: "Tarefas",
                column: "CondicaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Condicoes");
        }
    }
}
