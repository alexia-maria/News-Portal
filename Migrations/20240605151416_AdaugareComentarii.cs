using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB7_WEB.Migrations
{
    /// <inheritdoc />
    public partial class AdaugareComentarii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comentarii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StireId = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Continut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StireModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarii", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarii_Stiri_StireModelId",
                        column: x => x.StireModelId,
                        principalTable: "Stiri",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarii_StireModelId",
                table: "Comentarii",
                column: "StireModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarii");
        }
    }
}
