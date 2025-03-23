using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdFit.Data.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Pages_PageId",
                table: "Advertisements");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "Advertisements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Pages_PageId",
                table: "Advertisements",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Pages_PageId",
                table: "Advertisements");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Pages_PageId",
                table: "Advertisements",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
