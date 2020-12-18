using Microsoft.EntityFrameworkCore.Migrations;

namespace GymFinal.Migrations
{
    public partial class level20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "Lesson",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MembersID",
                table: "Lesson",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MembersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    studioClassID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MembersID);
                    table.ForeignKey(
                        name: "FK_Members_StudioClass_studioClassID",
                        column: x => x.studioClassID,
                        principalTable: "StudioClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_MembersID",
                table: "Lesson",
                column: "MembersID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_studioClassID",
                table: "Members",
                column: "studioClassID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Members_MembersID",
                table: "Lesson",
                column: "MembersID",
                principalTable: "Members",
                principalColumn: "MembersID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Members_MembersID",
                table: "Lesson");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_MembersID",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "MembersID",
                table: "Lesson");
        }
    }
}
