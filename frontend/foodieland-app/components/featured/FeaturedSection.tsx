import { fetchFeaturedRecipes } from "@/actions/featured";
import { Error, FeaturedSlider } from "@/components";
import { FeaturedRecipe } from "@/types";

export default async function FeaturedSection() {
    const featuredRecipesData: Array<FeaturedRecipe> | undefined = await fetchFeaturedRecipes();

    if (!featuredRecipesData) return (
        <Error errorMessage="Oops! An unexpected error occurred while fetching featured recipes. Please try again later." />
    );

    if (featuredRecipesData.length === 0) return (
        <Error errorMessage="Oops! There are no featured recipes available for now. Please try again later." />
    );

    return <FeaturedSlider recipes={featuredRecipesData} />;
}