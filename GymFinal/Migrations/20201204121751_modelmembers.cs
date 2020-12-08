using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymFinal.Migrations
{
    public partial class modelmembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    TrainersID = table.Column<int>(nullable: false),
                    TrainerName = table.Column<string>(nullable: true),
                    Seniority = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.TrainersID);
                });

            migrationBuilder.CreateTable(
                name: "StudioClass",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    TypeID = table.Column<int>(nullable: false),
                    DuringTime = table.Column<int>(nullable: false),
                    BurnCalories = table.Column<int>(nullable: false),
                    TrainersID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudioClass", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudioClass_Trainers_TrainersID",
                        column: x => x.TrainersID,
                        principalTable: "Trainers",
                        principalColumn: "TrainersID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    Type = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    LessonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerID = table.Column<int>(nullable: false),
                    StudioClassID = table.Column<int>(nullable: false),
                    MemberID = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    LessonDay = table.Column<string>(nullable: true),
                    LessonTime = table.Column<DateTime>(nullable: false),
                    MembersID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.LessonID);
                    table.ForeignKey(
                        name: "FK_Lesson_Members_MembersID",
                        column: x => x.MembersID,
                        principalTable: "Members",
                        principalColumn: "MembersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lesson_StudioClass_StudioClassID",
                        column: x => x.StudioClassID,
                        principalTable: "StudioClass",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lesson_Trainers_TrainerID",
                        column: x => x.TrainerID,
                        principalTable: "Trainers",
                        principalColumn: "TrainersID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_MembersID",
                table: "Lesson",
                column: "MembersID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_StudioClassID",
                table: "Lesson",
                column: "StudioClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_TrainerID",
                table: "Lesson",
                column: "TrainerID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_studioClassID",
                table: "Members",
                column: "studioClassID");

            migrationBuilder.CreateIndex(
                name: "IX_StudioClass_TrainersID",
                table: "StudioClass",
                column: "TrainersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "StudioClass");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
