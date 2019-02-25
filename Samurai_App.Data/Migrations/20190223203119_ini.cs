using Microsoft.EntityFrameworkCore.Migrations;

namespace Samurai_App.Data.Migrations
{
    public partial class ini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Samurais",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "ramy" });

            migrationBuilder.InsertData(
                table: "Quotes",
                columns: new[] { "Id", "SamuraiId", "Text" },
                values: new object[] { 1, 1, "great fighter" });

            migrationBuilder.InsertData(
                table: "Quotes",
                columns: new[] { "Id", "SamuraiId", "Text" },
                values: new object[] { 2, 1, "try hard" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Quotes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Samurais",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
