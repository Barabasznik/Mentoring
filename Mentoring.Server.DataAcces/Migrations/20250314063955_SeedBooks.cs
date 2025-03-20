using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mentoring.Server.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Description", "Title" },
                values: new object[,]
                {
                    { -4, "Andrew Hunt", "Droga do mistrzostwa", "Pragmatyczny Programista" },
                    { -3, "Thomas H. Cormen", "Wprowadzenie do algorytmiki", "Algorytmy" },
                    { -2, "Martin Fowler", "Poprawianie struktury kodu", "Refaktoryzacja" },
                    { -1, "Robert C. Martin", "Podręcznik dla programistów", "Czysty Kod" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
