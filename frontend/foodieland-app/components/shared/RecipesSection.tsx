import { Container, Description, RecipeCard, Title } from "@/components";

const recipeCards = [
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Meat",
        timeToCook: 30
    },
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Vegan",
        timeToCook: 20
    },
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Lunch",
        timeToCook: 10
    },
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Meat",
        timeToCook: 30
    },
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Vegan",
        timeToCook: 20
    },
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Lunch",
        timeToCook: 10
    },
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Meat",
        timeToCook: 30
    },
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Vegan",
        timeToCook: 20
    },
    {
        imagePath: "/featured-recipe.jpg",
        name: "Lorem ipsum dolor sit amet",
        category: "Lunch",
        timeToCook: 10
    },
];

export default function RecipesSection() {
    return (
        <Container className="w-full flex flex-col items-center gap-10 text-center">
            <Title text="Simple and tasty recipes" />
            <Description
                text="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad aut enim libero neque non quas repellat repellendus tenetur ullam voluptates?"
                className="max-w-2xl" />
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-10">
                {recipeCards.map((recipe, idx) => (
                    <RecipeCard {...recipe} key={idx} />
                ))}
            </div>
        </Container>
    );
}