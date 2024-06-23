using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace foodieland.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("02017150-a4cd-4154-9163-a16c7c61516a"), "Pancakes" },
                    { new Guid("29155b63-3e95-4f93-b4cc-df9b29a66ac8"), "Egg fried rice" },
                    { new Guid("8b61efe7-a575-4a6d-b61b-a8f10a0ac94a"), "Shepherd's pie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
