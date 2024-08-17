import { z } from "zod";
import { featuredRecipeSchema } from "@/schemas/featuredRecipe";

export type FeaturedRecipe = z.infer<typeof featuredRecipeSchema>;