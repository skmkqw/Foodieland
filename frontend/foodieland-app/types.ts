import { z } from "zod";
import { featuredRecipeSchema } from "@/schemas/featuredRecipe";
import { shortRecipeSchema } from "@/schemas/shortRecipe";

export type FeaturedRecipe = z.infer<typeof featuredRecipeSchema>;

export type RecipeShort = z.infer<typeof shortRecipeSchema>;

export type Recipe = {
    id: string,
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
    nutritionInformation: NutritionInformation,
    ingredients: Array<Ingredient>,
    directions: Array<Direction>
}

export type NutritionInformation = {
    calories: number,
    protein: number,
    fat: number,
    carbohydrate: number
}

export type RecipeCreator = {
    creatorId: string,
    creatorName: string,
    userImage: string | null
}


export type Ingredient = {
    name: string,
    amount: number,
    unit: string
}

export type Direction = {
    stepNumber: number,
    name: string,
    description: string
}