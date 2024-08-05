"use server";

import { z } from "zod";
import { axiosInstance } from "@/lib/axios";
import { RecipeProps, recipeSchema } from "@/schemas/recipe";
import { getSession } from "@/lib/session";

const recipeSchemaArray = z.array(recipeSchema);

export const fetchRecipes = async (recipeAmount: number): Promise<RecipeProps[] | undefined> => {
    const session = await getSession();
    let parsedData;
    try {
        if (session) {
            const response = await axiosInstance.get(`/recipes?page=1&pageSize=${recipeAmount}`, {
                headers: {
                    Authorization: `Bearer ${session}`
                }
            });
            parsedData = recipeSchemaArray.safeParse(response.data);
        } else {
            const response = await axiosInstance.get(`/recipes?page=1&pageSize=${recipeAmount}`);
            parsedData = recipeSchemaArray.safeParse(response.data);
        }

        if (!parsedData.success) {
            console.error("Invalid data structure:", parsedData.error);
        }

        return parsedData.data;
    } catch (error) {
        console.error("Error fetching recipes:", error);
    }
};

export const likeRecipe = async (recipeId: string) => {
    try {
        const session = await getSession();

        if (!session) {
            console.error("Failed to get session:", session);
            return false;
        }

        await axiosInstance.post(`/recipes/${recipeId}/like`, {}, {
            headers: {
                Authorization: `Bearer ${session}`
            }
        });

        return true;
    } catch (error) {
        console.error("Error liking recipe:", error);
        return false;
    }
};