using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAppProject.Migrations
{
    public partial class addPhaToPre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Pharmacies_PharmacistId",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "PharmacistId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Pharmacies_PharmacistId",
                table: "Prescriptions",
                column: "PharmacistId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Pharmacies_PharmacistId",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "PharmacistId",
                table: "Prescriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Pharmacies_PharmacistId",
                table: "Prescriptions",
                column: "PharmacistId",
                principalTable: "Pharmacies",
                principalColumn: "Id");
        }
    }
}
