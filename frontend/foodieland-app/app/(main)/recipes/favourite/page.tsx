import { Container, Error, FilterSidebar, RecipeGrid, Title } from "@/components";
import { fetchLikedRecipes } from "@/actions/recipes";
import { Suspense } from "react";

export default async function FavouritePage() {
    const recipes = await fetchLikedRecipes(1);
    const categories = recipes ? Array.from(new Set(recipes.map(recipe => recipe.category))) : [];

    return (
        <Container className="w-full py-10">
            <Title
                text="Recipes that bring happiness to your table ❤️"
                className="text-4xl lg:text-start text-center"
            />
            <div className="flex flex-col lg:grid lg:grid-cols-5 mt-10 gap-6">
                <FilterSidebar className="col-span-1" categories={categories} />
                {recipes && recipes.length != 0 ?
                    <Suspense fallback={<p>A</p>}>
                        <RecipeGrid recipes={recipes} />
                    </Suspense>
                    : <Error errorMessage="No recipes found." />
                }
            </div>
        </Container>
    );
}