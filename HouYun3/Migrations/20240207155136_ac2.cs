using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouYun3.Migrations
{
    /// <inheritdoc />
    public partial class ac2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SearchDate",
                table: "SearchHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchDate",
                table: "SearchHistories");
        }
    }
}
