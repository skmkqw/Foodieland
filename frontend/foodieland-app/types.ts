import { z } from "zod";
import { featuredRecipeSchema } from "@/schemas/featuredRecipe";
import { recipeSchema } from "@/schemas/recipe";

type FeaturedRecipe = z.infer<typeof featuredRecipeSchema>;

type RecipeShort = z.infer<typeof recipeSchema>;

export type Recipe = z.infer<typeof recipeSchema>;