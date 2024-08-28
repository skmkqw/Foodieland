import { Error, FilterSidebar, Pagination, RecipeGrid } from "@/components";
import { fetchLikedRecipes } from "@/actions/recipes";

export default async function FavouriteSection({ searchParams }) {
    let page = parseInt(searchParams.page);
    page = !page || page < 0 ? 1 : page;

    const nextPage = page + 1;
    const prevPage = page - 1 > 0 ? page - 1 : 1;

    const data = await fetchLikedRecipes(page);
    if (!data) {
        return <Error errorMessage="Failed to fetch recipes." />;
    }

    const { totalAmount, likedRecipes } = data;

    const totalPages = Math.ceil(totalAmount / 4);

    const categories = likedRecipes ? Array.from(new Set(likedRecipes.map(recipe => recipe.category))) : [];

    return (
        <div className="flex flex-col items-center">
            <div className="flex flex-col lg:grid lg:grid-cols-5 mt-10 gap-6">
                <FilterSidebar className="col-span-1" categories={categories} />
                {likedRecipes && likedRecipes.length != 0 ?
                    <RecipeGrid recipes={likedRecipes} />
                    : <Error errorMessage="No recipes found." />
                }
            </div>
            <Pagination
                activePage={page}
                prevPage={prevPage}
                nextPage={nextPage}
                totalPages={totalPages}
                className="mt-10"
            />
        </div>
    );
}