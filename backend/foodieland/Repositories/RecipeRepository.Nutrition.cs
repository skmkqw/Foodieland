using foodieland.DTO.NutritionInformation;
using foodieland.Entities;
using foodieland.Mappers;
using foodieland.Models;
using Microsoft.EntityFrameworkCore;

namespace foodieland.Repositories;

public partial class RecipeRepository
{
    public async Task<NutritionInformationEntity?> GetNutritionInformation(Guid recipeId)
    {
        var recipe = await _context.Recipes.FindAsync(recipeId);
        if (recipe == null)
        {
            return null;
        }

        var nutritionInformation = await _context.NutritionInformation.FirstOrDefaultAsync(ni => ni.RecipeId == recipeId);

        return nutritionInformation;
    }

    public async Task<(NutritionInformationEntity? nutritionInformation, string? error)> AddNutritionInformation(Guid recipeId, AddOrUpdateNutritionDto addNutritionInfoDto)
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

    public async Task<NutritionInformationEntity> ChangeNutritionInformation(Guid nutritionId, AddOrUpdateNutritionDto updateNutritionInfoDto)
    {
        NutritionInformationEntity nutritionInformationEntity = (await _context.NutritionInformation.FindAsync(nutritionId))!;
        _context.Entry(nutritionInformationEntity).CurrentValues.SetValues(updateNutritionInfoDto);
        await _context.SaveChangesAsync();
        return nutritionInformationEntity;
    }
}