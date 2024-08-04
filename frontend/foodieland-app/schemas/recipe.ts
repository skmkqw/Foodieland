import { z } from "zod";

export const recipeSchema = z.object({
    name: z.string(),
    description: z.string(),
    timeToCook: z.number(),
    category: z.string(),
    isLiked: z.boolean()
});

export type RecipeProps = z.infer<typeof recipeSchema>;