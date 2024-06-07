using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LAB7_WEB.Migrations
{
    /// <inheritdoc />
    public partial class migrationComentarii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarii_Stiri_StireModelId",
                table: "Comentarii");

            migrationBuilder.DropIndex(
                name: "IX_Comentarii_StireModelId",
                table: "Comentarii");

            migrationBuilder.DropColumn(
                name: "StireModelId",
                table: "Comentarii");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarii_StireId",
                table: "Comentarii",
                column: "StireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarii_Stiri_StireId",
                table: "Comentarii",
                column: "StireId",
                principalTable: "Stiri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarii_Stiri_StireId",
                table: "Comentarii");

            migrationBuilder.DropIndex(
                name: "IX_Comentarii_StireId",
                table: "Comentarii");

            migrationBuilder.AddColumn<int>(
                name: "StireModelId",
                table: "Comentarii",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarii_StireModelId",
                table: "Comentarii",
                column: "StireModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarii_Stiri_StireModelId",
                table: "Comentarii",
                column: "StireModelId",
                principalTable: "Stiri",
                principalColumn: "Id");
        }
    }
}
