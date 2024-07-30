import { fetchFeaturedRecipes } from "@/actions/featured";
import { FeaturedSlider } from "@/components";

export default async function FeaturedSection() {
    const featuredRecipesData = await fetchFeaturedRecipes();
    if (!featuredRecipesData) return <div>Failed.</div>;
    return (
        <div className="flex gap-12">
            <FeaturedSlider recipes={featuredRecipesData} />
        </div>
    );
}