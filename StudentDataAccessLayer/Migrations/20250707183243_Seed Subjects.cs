using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SeedSubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Study of numbers, algebra, geometry, and calculus.", "Mathematics" },
                    { 2, "Study of matter, energy, and their interactions.", "Physics" },
                    { 3, "Study of substances and chemical reactions.", "Chemistry" },
                    { 4, "Study of living organisms.", "Biology" },
                    { 5, "Language and literature studies.", "English" },
                    { 6, "Study of past events.", "History" },
                    { 7, "Study of Earth's surface and environment.", "Geography" },
                    { 8, "Study of programming, data, and systems.", "Computer Science" },
                    { 9, "Study of Islamic religion and teachings.", "Islamic Studies" },
                    { 10, "Study of human society and social relationships.", "Social Studies" },
                    { 11, "Visual and creative arts.", "Art" },
                    { 12, "Exercise, fitness, and sports.", "Physical Education" },
                    { 13, "General science including basic biology, chemistry, and physics.", "Science" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
