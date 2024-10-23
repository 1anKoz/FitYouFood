using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitYouFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectIngredientAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientsAmounts_IngredientAmountIngredientId_IngredientAmountMealId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_IngredientAmountIngredientId_IngredientAmountMealId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IngredientAmountIngredientId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IngredientAmountMealId",
                table: "Ingredients");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsAmounts_Ingredients_IngredientId",
                table: "IngredientsAmounts",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsAmounts_Ingredients_IngredientId",
                table: "IngredientsAmounts");

            migrationBuilder.AddColumn<int>(
                name: "IngredientAmountIngredientId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IngredientAmountMealId",
                table: "Ingredients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientAmountIngredientId_IngredientAmountMealId",
                table: "Ingredients",
                columns: new[] { "IngredientAmountIngredientId", "IngredientAmountMealId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientsAmounts_IngredientAmountIngredientId_IngredientAmountMealId",
                table: "Ingredients",
                columns: new[] { "IngredientAmountIngredientId", "IngredientAmountMealId" },
                principalTable: "IngredientsAmounts",
                principalColumns: new[] { "IngredientId", "MealId" });
        }
    }
}
