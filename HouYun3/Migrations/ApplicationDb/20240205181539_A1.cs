using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouYun3.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class A1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "SearchHistory");

            migrationBuilder.DropTable(
                name: "WatchHistory");

            migrationBuilder.DropTable(
                name: "WatchLater");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SearchHistory",
                columns: table => new
                {
                    SearchHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SearchQuery = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistory", x => x.SearchHistoryId);
                    table.ForeignKey(
                        name: "FK_SearchHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchHistory",
                columns: table => new
                {
                    WatchHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WatchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchHistory", x => x.WatchHistoryId);
                    table.ForeignKey(
                        name: "FK_WatchHistory_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchHistory_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "VideoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchLater",
                columns: table => new
                {
                    WatchLaterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WatchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchLater", x => x.WatchLaterId);
                    table.ForeignKey(
                        name: "FK_WatchLater_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchLater_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "VideoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchHistory_UserId",
                table: "SearchHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchHistory_UserId1",
                table: "WatchHistory",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_WatchHistory_VideoId",
                table: "WatchHistory",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLater_UserId1",
                table: "WatchLater",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_WatchLater_VideoId",
                table: "WatchLater",
                column: "VideoId");
        }
    }
}
