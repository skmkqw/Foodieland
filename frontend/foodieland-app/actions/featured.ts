"use server";

import { z } from "zod";
import { axiosInstance } from "@/lib/axios";
import { FeaturedRecipeProps, featuredRecipeSchema } from "@/schemas/featuredRecipe";

const featuredRecipesSchema = z.array(featuredRecipeSchema);

export const fetchFeaturedRecipes = async (): Promise<FeaturedRecipeProps[] | undefined> => {
    try {
        const response = await axiosInstance.get("/recipes/featured");
        const parsedData = featuredRecipesSchema.safeParse(response.data);

        if (!parsedData.success) {
            console.error("Invalid data structure:", parsedData.error);
        }

        return parsedData.data;
    } catch (error) {
        console.error("Error fetching featured recipes:", error);
    }
};