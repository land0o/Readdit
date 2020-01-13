using Microsoft.EntityFrameworkCore.Migrations;

namespace Readdit.Migrations
{
    public partial class Rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9e1c2178-3ea0-465a-ba4c-6b8fd78fba1b", "AQAAAAEAACcQAAAAEE640TuJTUMTWv/Y1xNbIEgDvXO7FmlQjcrITDvc2IGw/Q4lTlq5wltdFOGj2tZG6Q==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "16160ed2-41c7-44bc-8869-41f0927ed672", "AQAAAAEAACcQAAAAEPOyAFlbADMT2M7AAS9h9QT3AVP/leoLSGP1Sb7QXpj0rr5T49pkTj1mLV6qRGR5gA==" });

            migrationBuilder.CreateIndex(
                name: "IX_User_ApplicationUserId",
                table: "User",
                column: "ApplicationUserId");
        }
    }
}
