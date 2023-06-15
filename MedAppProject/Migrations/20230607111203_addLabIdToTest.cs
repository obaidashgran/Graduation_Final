using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAppProject.Migrations
{
    public partial class addLabIdToTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_LabId",
                table: "Tests",
                column: "LabId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Labs_LabId",
                table: "Tests",
                column: "LabId",
                principalTable: "Labs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Labs_LabId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_LabId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "LabId",
                table: "Tests");
        }
    }
}
