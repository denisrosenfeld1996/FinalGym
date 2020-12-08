using Microsoft.EntityFrameworkCore.Migrations;

namespace GymFinal.Migrations
{
    public partial class stats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatsViewModelid1",
                table: "StudioClass",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudioClass_StatsViewModelid1",
                table: "StudioClass",
                column: "StatsViewModelid1");

            migrationBuilder.AddForeignKey(
                name: "FK_StudioClass_StatsViewModel_StatsViewModelid1",
                table: "StudioClass",
                column: "StatsViewModelid1",
                principalTable: "StatsViewModel",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudioClass_StatsViewModel_StatsViewModelid1",
                table: "StudioClass");

            migrationBuilder.DropIndex(
                name: "IX_StudioClass_StatsViewModelid1",
                table: "StudioClass");

            migrationBuilder.DropColumn(
                name: "StatsViewModelid1",
                table: "StudioClass");
        }
    }
}
