import { Container, Description, RecipeCard, Title } from "@/components";
import { fetchRecipes } from "@/actions/recipes";


export default async function RecipesSection() {
    const recipes = await fetchRecipes(6);
    if (!recipes) return <p>Error loading recipes</p>;

    return (
        <Container className="w-full flex flex-col items-center gap-10 text-center">
            <Title text="Simple and tasty recipes" />
            <Description
                text="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad aut enim libero neque non quas repellat repellendus tenetur ullam voluptates?"
                className="max-w-2xl" />
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-10">
                {recipes.map((recipe, idx) => (
                    <RecipeCard imagePath="/featured-recipe.jpg" name={recipe.name} category={recipe.category}
                                timeToCook={recipe.timeToCook} key={idx} />
                ))}
            </div>
        </Container>
    );
}