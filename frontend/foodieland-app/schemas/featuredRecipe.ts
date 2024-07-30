import { z } from "zod";

export const featuredRecipeSchema = z.object({
    id: z.number(),
    name: z.string(),
    description: z.string(),
    cookingTime: z.number(),
    category: z.string(),
    creatorName: z.string(),
    creationDate: z.string(),
});

export type FeaturedRecipeProps = z.infer<typeof featuredRecipeSchema>;