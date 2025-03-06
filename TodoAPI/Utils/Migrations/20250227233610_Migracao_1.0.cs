using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migracao_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Listas",
                columns: table => new
                {
                    listaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    titulo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listas", x => x.listaId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    titulo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    dataInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    dataFim = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    listaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_Item_Listas_listaId",
                        column: x => x.listaId,
                        principalTable: "Listas",
                        principalColumn: "listaId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Item_listaId",
                table: "Item",
                column: "listaId");

            migrationBuilder.InsertData(
                table: "Listas",
                columns: new[] { "listaId", "titulo" },
                values: new object[,]
                {
                        { 1, "Lista de Compras" },
                        { 2, "Tarefas da Semana" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "itemId", "titulo", "descricao", "status", "dataInicio", "dataFim", "listaId" },
                values: new object[,]
                {
                    { 1, "Comprar leite", "Comprar leite integral no mercado", false, DateTime.Now, DateTime.Now.AddHours(2), 1 },
                    { 2, "Comprar pão", "Pão francês na padaria", false, DateTime.Now, DateTime.Now.AddHours(2), 1 },
                    { 3, "Comprar café", "Café moído de qualidade", false, DateTime.Now, DateTime.Now.AddHours(2), 1 },
                    { 4, "Estudar C#", "Revisar conceitos de controle de fluxo", false, DateTime.Now, DateTime.Now.AddHours(3), 2 },
                    { 5, "Fazer exercícios físicos", "Treino de força na academia", false, DateTime.Now, DateTime.Now.AddHours(2), 2 },
                    { 6, "Ler um livro", "Capítulo 5 do livro de desenvolvimento pessoal", false, DateTime.Now, DateTime.Now.AddHours(2), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Listas");
        }
    }
}
