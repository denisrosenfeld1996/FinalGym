using Microsoft.EntityFrameworkCore.Migrations;

namespace GymFinal.Migrations
{
    public partial class nision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatsViewModelid",
                table: "StudioClass",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StatsViewModel",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsersCount = table.Column<int>(nullable: false),
                    TrainersCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsViewModel", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudioClass_StatsViewModelid",
                table: "StudioClass",
                column: "StatsViewModelid");

            migrationBuilder.AddForeignKey(
                name: "FK_StudioClass_StatsViewModel_StatsViewModelid",
                table: "StudioClass",
                column: "StatsViewModelid",
                principalTable: "StatsViewModel",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudioClass_StatsViewModel_StatsViewModelid",
                table: "StudioClass");

            migrationBuilder.DropTable(
                name: "StatsViewModel");

            migrationBuilder.DropIndex(
                name: "IX_StudioClass_StatsViewModelid",
                table: "StudioClass");

            migrationBuilder.DropColumn(
                name: "StatsViewModelid",
                table: "StudioClass");
        }
    }
}
