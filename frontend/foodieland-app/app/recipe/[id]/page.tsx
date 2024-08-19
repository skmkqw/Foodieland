import { DirectionsSection, Error, Inbox, IngredientsSection, RecipeInfo } from "@/components";
import { RecipeExtended } from "@/types";
import { notFound } from "next/navigation";
import { fetchRecipe } from "@/actions/recipes";

export default async function RecipePage({ params }) {
    if (!params.id) return notFound();

    const recipe:RecipeExtended | undefined = await fetchRecipe(params.id);

    if (!recipe) return (
        <Error errorMessage="Oops! An unexpected error occurred while fetching recipe details. Please try again later." className="mt-10" />
    );

    return (
        <main className="flex flex-col py-5 sm:py-8 md:py-10">
            <RecipeInfo
                recipe={recipe.recipe}
                creator={recipe.creator}
                nutritionInformation={recipe.nutritionInformation}
            />
            <IngredientsSection ingredients={recipe.ingredients} />
            <DirectionsSection directions={recipe.directions} />
            <Inbox className="mt-12 md:mt-20 !px-6" />
        </main>
    );
}