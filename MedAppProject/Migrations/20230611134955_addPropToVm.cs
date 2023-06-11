using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAppProject.Migrations
{
    public partial class addPropToVm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "VMLogins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isVerified",
                table: "VMLogins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "VMLogins");

            migrationBuilder.DropColumn(
                name: "isVerified",
                table: "VMLogins");
        }
    }
}
