using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedAppProject.Migrations
{
    public partial class addDefVal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
            name: "DoctorRate",
            table: "Doctors",
            type: "real",
            nullable: false,
            defaultValue: 1.23f, // Add default value here
            oldClrType: typeof(double),
            oldType: "float",
            oldDefaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
            name: "DoctorRate",
            table: "Doctors",
            type: "float",
            nullable: false,
            defaultValue: 0.0,
            oldClrType: typeof(float),
            oldType: "real",
            oldDefaultValue: 1.23f);
        }
    }
}
