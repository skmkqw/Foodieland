"use server";

import { getSession } from "@/lib/session";
import { RecipeExtended, RecipeShort } from "@/types";
import { z } from "zod";
import { axiosInstance } from "@/lib/axios";
import { recipeExtendedSchema, shortRecipeSchema } from "@/schemas/recipes";
import { revalidatePath } from "next/cache";

const recipeSchemaArray = z.array(shortRecipeSchema);

interface FetchRecipesResponse {
    totalAmount: number;
    recipes: RecipeShort[];
}

export const fetchRecipes = async (page: number, pageSize: number): Promise<FetchRecipesResponse | undefined> => {
    const session = await getSession();
    try {
        const response = await axiosInstance.get(`/recipes/published?page=${page}&pageSize=${pageSize}`, {
            headers: session ? { Authorization: `Bearer ${session}` } : undefined
        });

        const totalAmount = response.data.totalRecipes;

        const parsedData = recipeSchemaArray.safeParse(response.data.recipes);

        if (!parsedData.success) {
            console.error("Invalid data structure:", parsedData.error);
            return;
        }

        const recipes = parsedData.data as RecipeShort[];

        return {
            totalAmount,
            recipes
        };
    } catch (error) {
        console.error("Error fetching recipes:", error);
    }
};

export const likeRecipe = async (recipeId: string): Promise<boolean> => {
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

        revalidatePath("/recipes/favourite");

        return true;
    } catch (error) {
        console.error("Error liking recipe:", error);
        return false;
    }
};

export const unlikeRecipe = async (recipeId: string): Promise<boolean> => {
    try {
        const session = await getSession();

        if (!session) {
            console.error("Failed to get session:", session);
            return false;
        }

        await axiosInstance.delete(`/recipes/${recipeId}/unlike`, {
            headers: {
                Authorization: `Bearer ${session}`
            }
        });

        revalidatePath("/recipes/favourite");

        return true;
    } catch (error) {
        console.error("Error unliking recipe:", error);
        return false;
    }
};

export const fetchRecipe = async (recipeId: string): Promise<RecipeExtended | undefined> => {
    const session = await getSession();
    try {
        const response = await axiosInstance.get(`/recipes/${recipeId}?displayNutrition=true&displayDirections=true&displayIngredients=true`, {
            headers: session ? { Authorization: `Bearer ${session}` } : undefined
        });

        const parsedData = recipeExtendedSchema.safeParse(response.data);

        if (!parsedData.success) {
            console.error("Invalid data structure:", parsedData.error);
            return;
        }

        return parsedData.data;
    } catch (error) {
        console.error("Error fetching recipes:", error);
    }
}

interface FetchLikedRecipesResponse {
    totalAmount: number;
    likedRecipes: RecipeShort[];
}

export const fetchLikedRecipes = async (page: number): Promise<FetchLikedRecipesResponse | undefined> => {
    const session = await getSession();
    try {
        const response = await axiosInstance.get(`/recipes/liked?page=${page}&pageSize=4`, {
            headers: session ? { Authorization: `Bearer ${session}` } : undefined
        });

        const totalAmount:number = response.data.totalAmount;

        const parsedData = recipeSchemaArray.safeParse(response.data.likedRecipes);

        if (!parsedData.success) {
            console.error("Invalid data structure:", parsedData.error);
            return;
        }

        const likedRecipes = parsedData.data as RecipeShort[];

        return {
            totalAmount,
            likedRecipes
        };
    } catch (error) {
        console.error("Error fetching recipes:", error);
    }
}