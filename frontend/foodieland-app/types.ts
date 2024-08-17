import { z } from "zod";
import { featuredRecipeSchema } from "@/schemas/featuredRecipe";
import { recipeSchema } from "@/schemas/recipe";

export type FeaturedRecipe = z.infer<typeof featuredRecipeSchema>;

export type Recipe = z.infer<typeof recipeSchema>;