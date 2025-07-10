using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotingApp.UI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPollOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollOption_Polls_PollId",
                table: "PollOption");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PollOption",
                table: "PollOption");

            migrationBuilder.RenameTable(
                name: "PollOption",
                newName: "PollOptions");

            migrationBuilder.RenameIndex(
                name: "IX_PollOption_PollId",
                table: "PollOptions",
                newName: "IX_PollOptions_PollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PollOptions",
                table: "PollOptions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PollOptions_Polls_PollId",
                table: "PollOptions",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollOptions_Polls_PollId",
                table: "PollOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PollOptions",
                table: "PollOptions");

            migrationBuilder.RenameTable(
                name: "PollOptions",
                newName: "PollOption");

            migrationBuilder.RenameIndex(
                name: "IX_PollOptions_PollId",
                table: "PollOption",
                newName: "IX_PollOption_PollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PollOption",
                table: "PollOption",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PollOption_Polls_PollId",
                table: "PollOption",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
