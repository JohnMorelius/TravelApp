 using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteTestDataProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE OR ALTER PROCEDURE dbo.spDeleteTestData
                    @seeded BIT,
                    @nrLanderAffected INT OUTPUT,
                    @nrAnvandareAffected INT OUTPUT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT @nrLanderAffected = COUNT(*) FROM Land WHERE Seeded = @seeded;
                    SELECT @nrAnvandareAffected = COUNT(*) FROM Anvandare WHERE Seeded = @seeded;

                    DELETE FROM Land WHERE Seeded = @seeded;
                    DELETE FROM Anvandare WHERE Seeded = @seeded;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS dbo.spDeleteTestData;");
        }
    }
}
