using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdFit.Data.Migrations
{
    /// <inheritdoc />
    public partial class addadmin0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AdminAdvertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdminAdvertisements_UserId",
                table: "AdminAdvertisements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdminAdvertisements_Users_UserId",
                table: "AdminAdvertisements",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdminAdvertisements_Users_UserId",
                table: "AdminAdvertisements");

            migrationBuilder.DropIndex(
                name: "IX_AdminAdvertisements_UserId",
                table: "AdminAdvertisements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AdminAdvertisements");
        }
    }
}
