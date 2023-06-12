using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Migrations
{
    /// <inheritdoc />
    public partial class VersionActualizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3f7a6f2d-b597-45f9-aa39-93823ce25f98");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "25f654c4-5d98-4bd9-a0f2-47ce8d74a289");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "997b97ac-b81f-41ab-9a2b-dc50f9b159d9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "bc5253e1-87de-49c0-8c16-8b55dc0f6419");
        }
    }
}
