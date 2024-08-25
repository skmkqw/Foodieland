import { Container, Error, FilterSidebar, RecipeCard, Title } from "@/components";
import { fetchLikedRecipes } from "@/actions/recipes";

const filterGroups = [
    {
        groupName: "Categories",
        filterNames: ["Luch", "Dinner", "Vegan", "Seafood"]
    },
    {
        groupName: "Cooking time",
        filterNames: ["< 5 min", "5-15 min", "15-30 min", "45+ min"]
    }
];

export default async function FavouritePage() {
    //TODO protect route

    const recipes = await fetchLikedRecipes(4);

    //TODO responsive design
    return (
        <Container className="w-full py-10">
            <Title text="Recipes that bring happiness to your table ❤️" className="text-4xl" />
            <div className="grid grid-cols-5 mt-10 gap-6">
                <FilterSidebar className="col-span-1" groups={filterGroups} />
                <div className="col-span-4">
                    {recipes && recipes.length != 0 ?
                        <div className="grid grid-cols-1 md:grid-cols-2 gap-10">
                            {recipes.map((recipe, idx) => (
                                <RecipeCard recipe={recipe} key={idx} />
                            ))}
                        </div>
                        : <Error errorMessage="Oops! An unexpected error occurred while fetching liked recipes. Please try again later." />
                    }
                </div>
            </div>
        </Container>
    );
}