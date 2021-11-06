using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PagoEfectivo.Api.Migrations
{
    public partial class PrimeraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    Id_Estado = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.Id_Estado);
                });

            migrationBuilder.CreateTable(
                name: "Promocion",
                columns: table => new
                {
                    Id_Promocion = table.Column<Guid>(type: "uuid", nullable: false),
                    Codigo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Id_Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocion", x => x.Id_Promocion);
                    table.ForeignKey(
                        name: "FK_Promocion_Estado_Id_Estado",
                        column: x => x.Id_Estado,
                        principalTable: "Estado",
                        principalColumn: "Id_Estado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estado",
                columns: new[] { "Id_Estado", "Descripcion" },
                values: new object[,]
                {
                    { 1, "GENERADO" },
                    { 2, "CANJEADO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_Id_Estado",
                table: "Promocion",
                column: "Id_Estado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promocion");

            migrationBuilder.DropTable(
                name: "Estado");
        }
    }
}
