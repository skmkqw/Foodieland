import { Suspense } from "react";
import { DirectionsSection, Error, IngredientsSection, RecipeInfo, RecipePageSkeleton } from "@/components";
import { RecipeExtended } from "@/types";
import { fetchRecipe } from "@/actions/recipes";

export default async function RecipeContainer({ id }: { id: string }) {
    
    const recipe: RecipeExtended | undefined = await fetchRecipe(id);

    const wait = await new Promise(resolve => setTimeout(resolve, 1000));

    if (!recipe) return (
        <Error errorMessage="Oops! An unexpected error occurred while fetching recipe details. Please try again later."
               className="mt-10" />
    );

    return (
        <Suspense fallback={<RecipePageSkeleton />}>
            <RecipeInfo
                recipe={recipe.recipe}
                creator={recipe.creator}
                nutritionInformation={recipe.nutritionInformation}
            />
            <IngredientsSection ingredients={recipe.ingredients} />
            <DirectionsSection directions={recipe.directions} />
        </Suspense>
    );
}

