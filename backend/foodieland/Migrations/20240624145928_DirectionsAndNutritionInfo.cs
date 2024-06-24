using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace foodieland.Migrations
{
    /// <inheritdoc />
    public partial class DirectionsAndNutritionInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CookingDirection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descirption = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingDirection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookingDirection_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NutritionInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    Fat = table.Column<double>(type: "float", nullable: false),
                    Protein = table.Column<double>(type: "float", nullable: false),
                    Carbohydrate = table.Column<double>(type: "float", nullable: false),
                    Cholesterol = table.Column<double>(type: "float", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionInformation_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CookingDirection_RecipeId",
                table: "CookingDirection",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionInformation_RecipeId",
                table: "NutritionInformation",
                column: "RecipeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookingDirection");

            migrationBuilder.DropTable(
                name: "NutritionInformation");
        }
    }
}
