﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "User", "User".ToUpper(), Guid.NewGuid().ToString() }
                );

            migrationBuilder.InsertData(
              table: "AspNetRoles",
              columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
              values: new object[] { Guid.NewGuid().ToString(), "Admin", "Admin".ToUpper(), Guid.NewGuid().ToString() });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles] WHERE [NormalizedName] IN ('ADMIN', 'USER')");
        }
    }
}
