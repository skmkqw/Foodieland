using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace foodieland.Migrations
{
    /// <inheritdoc />
    public partial class CookingDirectionsTableRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookingDirection_Recipes_RecipeId",
                table: "CookingDirection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CookingDirection",
                table: "CookingDirection");

            migrationBuilder.RenameTable(
                name: "CookingDirection",
                newName: "CookingDirections");

            migrationBuilder.RenameIndex(
                name: "IX_CookingDirection_RecipeId",
                table: "CookingDirections",
                newName: "IX_CookingDirections_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CookingDirections",
                table: "CookingDirections",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CookingDirections_Recipes_RecipeId",
                table: "CookingDirections",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookingDirections_Recipes_RecipeId",
                table: "CookingDirections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CookingDirections",
                table: "CookingDirections");

            migrationBuilder.RenameTable(
                name: "CookingDirections",
                newName: "CookingDirection");

            migrationBuilder.RenameIndex(
                name: "IX_CookingDirections_RecipeId",
                table: "CookingDirection",
                newName: "IX_CookingDirection_RecipeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CookingDirection",
                table: "CookingDirection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CookingDirection_Recipes_RecipeId",
                table: "CookingDirection",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
