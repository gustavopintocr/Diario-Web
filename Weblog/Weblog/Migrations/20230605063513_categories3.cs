using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Migrations
{
    /// <inheritdoc />
    public partial class categories3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "62a1c163-db94-49fd-9650-f7fa21132161");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e6a8da7e-dd0c-4338-9c3d-61d39135b745");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "77fa7a3d-e8b9-4755-bf9a-c8abfc602451");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "988a9121-91c9-4e6a-99e1-ecfff2978e46");
        }
    }
}
