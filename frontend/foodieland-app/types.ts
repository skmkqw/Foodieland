import { z } from "zod";
import { featuredRecipeSchema } from "@/schemas/featuredRecipe";
import { recipeSchema } from "@/schemas/recipe";

type FeaturedRecipe = z.infer<typeof featuredRecipeSchema>;

type RecipeShort = z.infer<typeof recipeSchema>;

type Recipe = {
    name: string,
    description: string,
    creationDate: string,
    category: string,
    timeToCook: number,
    imageData: string | null
}

type RecipeExtended = {
    recipe: Recipe,
    creator: RecipeCreator,
    nutritionInformation: NutritionInformation
}

type NutritionInformation = {
    calories: number,
    protein: number,
    fat: number,
    carbohydrate: number,
    cholesterol: number
}

type RecipeCreator = {
    creatorName: string,
    userImage: string | null
}

export {
    RecipeShort,
    FeaturedRecipe,
    RecipeExtended,
    RecipeCreator,
    NutritionInformation,
}