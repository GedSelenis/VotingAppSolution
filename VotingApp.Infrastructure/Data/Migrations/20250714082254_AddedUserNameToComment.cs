using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotingApp.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserNameToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Comments");
        }
    }
}
