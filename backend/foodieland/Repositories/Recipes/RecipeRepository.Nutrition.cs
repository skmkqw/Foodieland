using foodieland.DTO.NutritionInformation;
using foodieland.Mappers;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories.Recipes;

public partial class RecipeRepository
{
    public async Task<NutritionInformation?> GetNutritionInformation(Guid recipeId)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            return null;
        }

        var nutritionInformation = await _context.NutritionInformation.FirstOrDefaultAsync(ni => ni.RecipeId == recipeId);

        return nutritionInformation;
    }

    public async Task<(NutritionInformation? nutritionInformation, string? error)> AddNutritionInformation(Recipe recipe, AddOrUpdateNutritionDto addNutritionInfoDto)
    {

        if (await GetNutritionInformation(recipe.Id) != null)
        {
            return (null, "Recipe already has nutrition information");
        }

        var createdInfo = await _context.NutritionInformation
            .AddAsync(addNutritionInfoDto.FromAddOrUpdateDtoToNutritionInformation(recipe.Id));
        await _context.SaveChangesAsync();
        return (createdInfo.Entity, null);
    }

    public async Task<NutritionInformation> ChangeNutritionInformation(NutritionInformation nutritionInformation, AddOrUpdateNutritionDto updateNutritionInfoDto)
    {
        _context.Entry(nutritionInformation).CurrentValues.SetValues(updateNutritionInfoDto);
        await _context.SaveChangesAsync();
        return nutritionInformation;
    }
}