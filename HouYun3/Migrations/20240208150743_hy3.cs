using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouYun3.Migrations
{
    /// <inheritdoc />
    public partial class hy3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WatchLaterItems_VideoId",
                table: "WatchLaterItems");

            migrationBuilder.DropIndex(
                name: "IX_WatchHistories_VideoId",
                table: "WatchHistories");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLaterItems_VideoId",
                table: "WatchLaterItems",
                column: "VideoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchHistories_VideoId",
                table: "WatchHistories",
                column: "VideoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WatchLaterItems_VideoId",
                table: "WatchLaterItems");

            migrationBuilder.DropIndex(
                name: "IX_WatchHistories_VideoId",
                table: "WatchHistories");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLaterItems_VideoId",
                table: "WatchLaterItems",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchHistories_VideoId",
                table: "WatchHistories",
                column: "VideoId");
        }
    }
}
