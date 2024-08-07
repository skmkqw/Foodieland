import { z } from "zod";

export const featuredRecipeSchema = z.object({
    name: z.string(),
    description: z.string(),
    timeToCook: z.number(),
    category: z.string(),
    creatorName: z.string(),
    creationDate: z.string(),
    imageData: z.string().nullable(),
    userImage: z.string().nullable()
});

export type FeaturedRecipeProps = z.infer<typeof featuredRecipeSchema>;