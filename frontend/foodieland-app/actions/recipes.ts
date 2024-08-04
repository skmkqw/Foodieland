"use server";

import { z } from "zod";
import { axiosInstance } from "@/lib/axios";
import { RecipeProps, recipeSchema } from "@/schemas/recipe";

const featuredRecipesSchema = z.array(recipeSchema);

export const fetchRecipes = async (recipeAmount: number): Promise<RecipeProps[] | undefined> => {
    try {
        const response = await axiosInstance.get(`/recipes?page=1&pageSize=${recipeAmount}`);
        const parsedData = featuredRecipesSchema.safeParse(response.data);

        if (!parsedData.success) {
            console.error("Invalid data structure:", parsedData.error);
        }

        return parsedData.data;
    } catch (error) {
        console.error("Error fetching recipes:", error);
    }
};