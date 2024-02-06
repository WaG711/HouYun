using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouYun3.Migrations
{
    /// <inheritdoc />
    public partial class Initialh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Videos_VideoID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserID",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Videos_VideoID",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchHistories_Users_UserID",
                table: "SearchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categories_CategoryID",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Users_UserID",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Users_UserID",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Videos_VideoID",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_Users_UserID",
                table: "WatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_Videos_VideoID",
                table: "WatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaters_Users_UserID",
                table: "WatchLaters");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaters_Videos_VideoID",
                table: "WatchLaters");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "WatchLaters",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "WatchLaters",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "WatchLaterID",
                table: "WatchLaters",
                newName: "WatchLaterId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchLaters_VideoID",
                table: "WatchLaters",
                newName: "IX_WatchLaters_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchLaters_UserID",
                table: "WatchLaters",
                newName: "IX_WatchLaters_UserId");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "WatchHistories",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "WatchHistories",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "WatchHistoryID",
                table: "WatchHistories",
                newName: "WatchHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistories_VideoID",
                table: "WatchHistories",
                newName: "IX_WatchHistories_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistories_UserID",
                table: "WatchHistories",
                newName: "IX_WatchHistories_UserId");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "Views",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Views",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ViewID",
                table: "Views",
                newName: "ViewId");

            migrationBuilder.RenameIndex(
                name: "IX_Views_VideoID",
                table: "Views",
                newName: "IX_Views_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Views_UserID",
                table: "Views",
                newName: "IX_Views_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Videos",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Videos",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "Videos",
                newName: "VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_UserID",
                table: "Videos",
                newName: "IX_Videos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_CategoryID",
                table: "Videos",
                newName: "IX_Videos_CategoryId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "SearchHistories",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "SearchHistoryID",
                table: "SearchHistories",
                newName: "SearchHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SearchHistories_UserID",
                table: "SearchHistories",
                newName: "IX_SearchHistories_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Notifications",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "NotificationID",
                table: "Notifications",
                newName: "NotificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "Likes",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Likes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "LikeID",
                table: "Likes",
                newName: "LikeId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_VideoID",
                table: "Likes",
                newName: "IX_Likes_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserID",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameColumn(
                name: "VideoID",
                table: "Comments",
                newName: "VideoId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CommentID",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_VideoID",
                table: "Comments",
                newName: "IX_Comments_VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Videos_VideoId",
                table: "Comments",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Videos_VideoId",
                table: "Likes",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchHistories_Users_UserId",
                table: "SearchHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categories_CategoryId",
                table: "Videos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Users_UserId",
                table: "Videos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Users_UserId",
                table: "Views",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Videos_VideoId",
                table: "Views",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_Users_UserId",
                table: "WatchHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_Videos_VideoId",
                table: "WatchHistories",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaters_Users_UserId",
                table: "WatchLaters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaters_Videos_VideoId",
                table: "WatchLaters",
                column: "VideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Videos_VideoId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Users_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Videos_VideoId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchHistories_Users_UserId",
                table: "SearchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Categories_CategoryId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Users_UserId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Users_UserId",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Videos_VideoId",
                table: "Views");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_Users_UserId",
                table: "WatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_Videos_VideoId",
                table: "WatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaters_Users_UserId",
                table: "WatchLaters");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchLaters_Videos_VideoId",
                table: "WatchLaters");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "WatchLaters",
                newName: "VideoID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WatchLaters",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "WatchLaterId",
                table: "WatchLaters",
                newName: "WatchLaterID");

            migrationBuilder.RenameIndex(
                name: "IX_WatchLaters_VideoId",
                table: "WatchLaters",
                newName: "IX_WatchLaters_VideoID");

            migrationBuilder.RenameIndex(
                name: "IX_WatchLaters_UserId",
                table: "WatchLaters",
                newName: "IX_WatchLaters_UserID");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "WatchHistories",
                newName: "VideoID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WatchHistories",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "WatchHistoryId",
                table: "WatchHistories",
                newName: "WatchHistoryID");

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistories_VideoId",
                table: "WatchHistories",
                newName: "IX_WatchHistories_VideoID");

            migrationBuilder.RenameIndex(
                name: "IX_WatchHistories_UserId",
                table: "WatchHistories",
                newName: "IX_WatchHistories_UserID");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Views",
                newName: "VideoID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Views",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "ViewId",
                table: "Views",
                newName: "ViewID");

            migrationBuilder.RenameIndex(
                name: "IX_Views_VideoId",
                table: "Views",
                newName: "IX_Views_VideoID");

            migrationBuilder.RenameIndex(
                name: "IX_Views_UserId",
                table: "Views",
                newName: "IX_Views_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Videos",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Videos",
                newName: "CategoryID");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Videos",
                newName: "VideoID");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_UserId",
                table: "Videos",
                newName: "IX_Videos_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_CategoryId",
                table: "Videos",
                newName: "IX_Videos_CategoryID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SearchHistories",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "SearchHistoryId",
                table: "SearchHistories",
                newName: "SearchHistoryID");

            migrationBuilder.RenameIndex(
                name: "IX_SearchHistories_UserId",
                table: "SearchHistories",
                newName: "IX_SearchHistories_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Notifications",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "Notifications",
                newName: "NotificationID");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserID");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Likes",
                newName: "VideoID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Likes",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "LikeId",
                table: "Likes",
                newName: "LikeID");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_VideoId",
                table: "Likes",
                newName: "IX_Likes_VideoID");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                newName: "IX_Likes_UserID");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Comments",
                newName: "VideoID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "CommentID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_VideoId",
                table: "Comments",
                newName: "IX_Comments_VideoID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_UserID");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Videos_VideoID",
                table: "Comments",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Users_UserID",
                table: "Likes",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Videos_VideoID",
                table: "Likes",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchHistories_Users_UserID",
                table: "SearchHistories",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Categories_CategoryID",
                table: "Videos",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Users_UserID",
                table: "Videos",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Users_UserID",
                table: "Views",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Videos_VideoID",
                table: "Views",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_Users_UserID",
                table: "WatchHistories",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_Videos_VideoID",
                table: "WatchHistories",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaters_Users_UserID",
                table: "WatchLaters",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchLaters_Videos_VideoID",
                table: "WatchLaters",
                column: "VideoID",
                principalTable: "Videos",
                principalColumn: "VideoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
