import { Container, Description, Error, RecipeCard, Title } from "@/components";
import { fetchRecipes } from "@/actions/recipes";


export default async function RecipesSection() {
    const data = await fetchRecipes(1, 6);
    if (!data) {
        return <Error errorMessage="Failed to fetch recipes." />;
    }

    const { totalAmount, recipes } = data;

    if (!recipes) return (
        <Error errorMessage="Oops! An unexpected error occurred while fetching recipes. Please try again later." />
    );

    if (recipes.length === 0) return (
        <Error errorMessage="Oops! There are no recipes available for now. Please try again later." />
    );

    return (
        <Container className="w-full flex flex-col items-center gap-10 text-center">
            <Title text="Simple and tasty recipes" />
            <Description
                text="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad aut enim libero neque non quas repellat repellendus tenetur ullam voluptates?"
                className="max-w-2xl"
            />
            <div className="w-full grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-10">
                {recipes.map((recipe, idx) => (
                    <RecipeCard recipe={recipe} key={idx} />
                ))}
            </div>
        </Container>
    );
}