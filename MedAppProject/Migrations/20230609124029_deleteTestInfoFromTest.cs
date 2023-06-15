using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAppProject.Migrations
{
    public partial class deleteTestInfoFromTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_testLabInfos_TestInfoId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestInfoId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestInfoId",
                table: "Tests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestInfoId",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestInfoId",
                table: "Tests",
                column: "TestInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_testLabInfos_TestInfoId",
                table: "Tests",
                column: "TestInfoId",
                principalTable: "testLabInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
