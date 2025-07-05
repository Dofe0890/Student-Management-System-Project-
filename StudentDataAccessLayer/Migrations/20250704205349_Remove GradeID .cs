using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDomainLayer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGradeID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeID",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradeID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
