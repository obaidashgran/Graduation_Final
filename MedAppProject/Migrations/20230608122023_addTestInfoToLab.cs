using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAppProject.Migrations
{
    public partial class addTestInfoToLab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LabId",
                table: "testLabInfos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_testLabInfos_LabId",
                table: "testLabInfos",
                column: "LabId");

            migrationBuilder.AddForeignKey(
                name: "FK_testLabInfos_Labs_LabId",
                table: "testLabInfos",
                column: "LabId",
                principalTable: "Labs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_testLabInfos_Labs_LabId",
                table: "testLabInfos");

            migrationBuilder.DropIndex(
                name: "IX_testLabInfos_LabId",
                table: "testLabInfos");

            migrationBuilder.DropColumn(
                name: "LabId",
                table: "testLabInfos");
        }
    }
}
