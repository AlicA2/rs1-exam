using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIT_Api_Examples.Migrations
{
    public partial class migracija4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpisAkGodine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datumUpisa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    godinaStudija = table.Column<int>(type: "int", nullable: false),
                    akademskaGodina_id = table.Column<int>(type: "int", nullable: true),
                    cijenaSkolarine = table.Column<float>(type: "real", nullable: false),
                    obnova = table.Column<bool>(type: "bit", nullable: false),
                    datumOvjere = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    student_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisAkGodine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpisAkGodine_AkademskaGodina_akademskaGodina_id",
                        column: x => x.akademskaGodina_id,
                        principalTable: "AkademskaGodina",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UpisAkGodine_Student_student_id",
                        column: x => x.student_id,
                        principalTable: "Student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpisAkGodine_akademskaGodina_id",
                table: "UpisAkGodine",
                column: "akademskaGodina_id");

            migrationBuilder.CreateIndex(
                name: "IX_UpisAkGodine_student_id",
                table: "UpisAkGodine",
                column: "student_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpisAkGodine");
        }
    }
}
