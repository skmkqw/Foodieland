import { z } from "zod";

export const shortRecipeSchema = z.object({
    id: z.string(),
    name: z.string(),
    description: z.string(),
    timeToCook: z.number(),
    category: z.string(),
    isLiked: z.boolean(),
    imageData: z.string().nullable()
});