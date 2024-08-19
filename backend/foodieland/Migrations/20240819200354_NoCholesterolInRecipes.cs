using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace foodieland.Migrations
{
    /// <inheritdoc />
    public partial class NoCholesterolInRecipes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cholesterol",
                table: "NutritionInformation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cholesterol",
                table: "NutritionInformation",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
