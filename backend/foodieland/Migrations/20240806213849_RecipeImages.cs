using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace foodieland.Migrations
{
    /// <inheritdoc />
    public partial class RecipeImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeaturedRecipes_RecipeId",
                table: "FeaturedRecipes");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Recipes",
                type: "VARBINARY(MAX)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedRecipes_RecipeId",
                table: "FeaturedRecipes",
                column: "RecipeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeaturedRecipes_RecipeId",
                table: "FeaturedRecipes");

            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Recipes");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedRecipes_RecipeId",
                table: "FeaturedRecipes",
                column: "RecipeId");
        }
    }
}
