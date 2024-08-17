import { z } from "zod";
import { featuredRecipeSchema } from "@/schemas/featuredRecipe";
import { recipeSchema } from "@/schemas/recipe";

export type FeaturedRecipe = z.infer<typeof featuredRecipeSchema>;

export type RecipeShort = z.infer<typeof recipeSchema>;

export type Recipe = {
    name: string,
    description: string,
    creationDate: string,
    category: string,
    timeToCook: number,
    imageData: string | null
}

export type RecipeExtended = {
    recipe: Recipe,
    creator: RecipeCreator,
    nutritionInformation: NutritionInformation
}

export type NutritionInformation = {
    calories: number,
    protein: number,
    fat: number,
    carbohydrate: number,
    cholesterol: number
}

export type RecipeCreator = {
    creatorName: string,
    userImage: string | null
}


export type Ingredient = {
    name: string,
    amount: number,
    unit: string
}