using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblog.Migrations
{
    /// <inheritdoc />
    public partial class Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryPublication",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "int", nullable: false),
                    UsersId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPublication", x => new { x.UsersId, x.UsersId1 });
                    table.ForeignKey(
                        name: "FK_CategoryPublication_Category_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPublication_Publication_UsersId1",
                        column: x => x.UsersId1,
                        principalTable: "Publication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e4ffceb9-00e7-4069-ad5b-33b95a5a41b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "14e2b727-8b8e-48ce-a92c-072c7f43f6be");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPublication_UsersId1",
                table: "CategoryPublication",
                column: "UsersId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPublication");

            migrationBuilder.DropTable(
                name: "Category");

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
