import { fetchFeaturedRecipes } from "@/actions/featured";
import { Container, FeaturedSlider } from "@/components";

export default async function FeaturedSection() {
    const featuredRecipesData = await fetchFeaturedRecipes();

    if (!featuredRecipesData) return (
        <Container>
            <div className="rounded-3xl bg-primary flex items-center justify-center p-10 text-center">
                <p className="font-medium text-2xl">Oops! An unexpected error occurred while fetching featured recipes. Please try again later.</p>
            </div>
        </Container>
    );

    return <FeaturedSlider recipes={featuredRecipesData} />;
}