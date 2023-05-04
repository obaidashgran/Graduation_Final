using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAppProject.Migrations
{
    /// <inheritdoc />
    public partial class RoleAndUserIdinVMLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "VMLogins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "VMLogins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.RestartSequence(
                name: "UserIdSequence",
                schema: "shared",
                startValue: 100L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "VMLogins");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "VMLogins");

            migrationBuilder.RestartSequence(
                name: "UserIdSequence",
                schema: "shared",
                startValue: 1L);
        }
    }
}
