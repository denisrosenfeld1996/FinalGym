using Microsoft.EntityFrameworkCore.Migrations;

namespace GymFinal.Migrations
{
    public partial class level30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Trainers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "StudioClass",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "StudioClass");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Trainers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
