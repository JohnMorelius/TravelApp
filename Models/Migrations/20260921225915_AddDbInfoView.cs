using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class AddDbInfoView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
               migrationBuilder.Sql(@"
    CREATE VIEW dbo.vwDbInfo AS
    SELECT
        (SELECT COUNT(*) FROM Land)      AS NrLand,
        (SELECT COUNT(*) FROM Ort)       AS NrOrter,
        (SELECT COUNT(*) FROM Sevardhet) AS NrSevardheter,
        (SELECT COUNT(*) FROM Anvandare) AS NrAnvandare,
        (SELECT COUNT(*) FROM Kommentar) AS NrKommentarer;
        "); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS dbo.vwDbInfo;");
        }
    }
}
