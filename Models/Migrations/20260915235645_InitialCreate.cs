using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anvandare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Epost = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anvandare", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Land",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Land", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ort",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ort_Land_LandId",
                        column: x => x.LandId,
                        principalTable: "Land",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sevardhet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rubrik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kategori = table.Column<int>(type: "int", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrtId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sevardhet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sevardhet_Ort_OrtId",
                        column: x => x.OrtId,
                        principalTable: "Ort",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kommentar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skapad = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SevardhetId = table.Column<int>(type: "int", nullable: false),
                    AnvandareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kommentar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kommentar_Anvandare_AnvandareId",
                        column: x => x.AnvandareId,
                        principalTable: "Anvandare",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kommentar_Sevardhet_SevardhetId",
                        column: x => x.SevardhetId,
                        principalTable: "Sevardhet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anvandare_Epost",
                table: "Anvandare",
                column: "Epost",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Kommentar_AnvandareId",
                table: "Kommentar",
                column: "AnvandareId");

            migrationBuilder.CreateIndex(
                name: "IX_Kommentar_SevardhetId",
                table: "Kommentar",
                column: "SevardhetId");

            migrationBuilder.CreateIndex(
                name: "IX_Ort_LandId",
                table: "Ort",
                column: "LandId");

            migrationBuilder.CreateIndex(
                name: "IX_Sevardhet_Kategori",
                table: "Sevardhet",
                column: "Kategori");

            migrationBuilder.CreateIndex(
                name: "IX_Sevardhet_OrtId",
                table: "Sevardhet",
                column: "OrtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kommentar");

            migrationBuilder.DropTable(
                name: "Anvandare");

            migrationBuilder.DropTable(
                name: "Sevardhet");

            migrationBuilder.DropTable(
                name: "Ort");

            migrationBuilder.DropTable(
                name: "Land");
        }
    }
}
