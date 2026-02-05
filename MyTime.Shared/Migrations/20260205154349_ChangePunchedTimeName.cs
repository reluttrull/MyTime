using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTime.Shared.Migrations
{
    /// <inheritdoc />
    public partial class ChangePunchedTimeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PunchedTime",
                table: "Punches",
                newName: "PunchedTimeUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PunchedTimeUtc",
                table: "Punches",
                newName: "PunchedTime");
        }
    }
}
