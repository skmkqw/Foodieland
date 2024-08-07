using foodieland.DTO.NutritionInformation;
using foodieland.Mappers;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories;

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

    public async Task<(NutritionInformation? nutritionInformation, string? error)> AddNutritionInformation(Guid recipeId, AddOrUpdateNutritionDto addNutritionInfoDto)
    {
        var recipe = await _context.Recipes.Include(ni => ni.NutritionInformation).FirstOrDefaultAsync(r => r.Id == recipeId);
        if (recipe == null)
        {
            return (null, "Recipe not found");
        }

        if (recipe.NutritionInformation != null)
        {
            return (null, "Recipe already has nutrition information");
        }

        var createdInfo =
            await _context.AddAsync(addNutritionInfoDto.FromAddOrUpdateDtoToNutritionInformation(recipeId));
        await _context.SaveChangesAsync();
        return (createdInfo.Entity, null);
    }

    public async Task<NutritionInformation> ChangeNutritionInformation(Guid nutritionId, AddOrUpdateNutritionDto updateNutritionInfoDto)
    {
        NutritionInformation nutritionInformation = (await _context.NutritionInformation.FindAsync(nutritionId))!;
        _context.Entry(nutritionInformation).CurrentValues.SetValues(updateNutritionInfoDto);
        await _context.SaveChangesAsync();
        return nutritionInformation;
    }
}