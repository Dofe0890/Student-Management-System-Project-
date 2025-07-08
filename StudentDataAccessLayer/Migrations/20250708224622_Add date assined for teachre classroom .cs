using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Adddateassinedforteachreclassroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedDate",
                table: "TeacherClasses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedDate",
                table: "TeacherClasses");
        }
    }
}
