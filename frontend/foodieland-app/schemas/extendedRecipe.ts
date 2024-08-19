import { z } from "zod";

const recipeSchema = z.object({
    id: z.string().uuid(),
    name: z.string().min(1),
    description: z.string().min(1),
    creationDate: z.string().refine((date) => !isNaN(Date.parse(date)), {
        message: "Invalid date format",
    }),
    category: z.string().min(1),
    timeToCook: z.number().nonnegative(),
    imageData: z.string().nullable(),
});

const recipeCreatorSchema = z.object({
    creatorId: z.string().uuid(),
    creatorName: z.string().min(1),
    userImage: z.string().nullable(),
});

const nutritionInformationSchema = z.object({
    calories: z.number().nonnegative(),
    protein: z.number().nonnegative(),
    fat: z.number().nonnegative(),
    carbohydrate: z.number().nonnegative(),
});

const ingredientSchema = z.object({
    name: z.string().min(1),
    amount: z.number().nonnegative(),
    unit: z.string().min(1),
});

const directionSchema = z.object({
    stepNumber: z.number().int().nonnegative(),
    name: z.string().min(1),
    description: z.string().min(1),
});

export const recipeExtendedSchema = z.object({
    recipe: recipeSchema,
    creator: recipeCreatorSchema,
    nutritionInformation: nutritionInformationSchema,
    ingredients: z.array(ingredientSchema),
    directions: z.array(directionSchema),
});