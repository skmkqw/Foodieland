import { Error, FilterSidebar, Pagination, RecipeGrid } from "@/components";
import { fetchLikedRecipes } from "@/actions/recipes";

export default async function FavouriteSection({ searchParams }) {
    let page = parseInt(searchParams.page);
    page = !page || page < 0 ? 1 : page;

    const nextPage = page + 1;
    const prevPage = page - 1 > 0 ? page - 1 : 1;

    const recipes = await fetchLikedRecipes(page);
    const categories = recipes ? Array.from(new Set(recipes.map(recipe => recipe.category))) : [];

    return (
        <div className="flex flex-col items-center">
            <div className="flex flex-col lg:grid lg:grid-cols-5 mt-10 gap-6">
                <FilterSidebar className="col-span-1" categories={categories} />
                {recipes && recipes.length != 0 ?
                    <RecipeGrid recipes={recipes} />
                    : <Error errorMessage="No recipes found." />
                }
            </div>
            <Pagination
                activePage={page}
                prevPage={prevPage}
                nextPage={nextPage}
                totalPages={3}
                className="mt-10"
            />
        </div>
    );
}