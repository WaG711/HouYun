using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouYun3.Migrations
{
    /// <inheritdoc />
    public partial class Hou2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_User_UserId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_User_UserId",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_User_UserId1",
                table: "WatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaters_User_UserId1",
                table: "WatchLaters");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "WatchLaters",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "WatchHistories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Views",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_User_UserId",
                table: "Videos",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Views_User_UserId",
                table: "Views",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_User_UserId1",
                table: "WatchHistories",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaters_User_UserId1",
                table: "WatchLaters",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_User_UserId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_User_UserId",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_User_UserId1",
                table: "WatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaters_User_UserId1",
                table: "WatchLaters");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "WatchLaters",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "WatchHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Views",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_User_UserId",
                table: "Videos",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_User_UserId",
                table: "Views",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_User_UserId1",
                table: "WatchHistories",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaters_User_UserId1",
                table: "WatchLaters",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
