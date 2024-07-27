import { Container, FeaturedRecipe, NavBar } from "@/components";

const featuredRecipes = [
    {
        id: 1,
        name: "Some green bullshit",
        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consectetur culpa dicta distinctio dolore eos error laboriosam modi quas quidem quisquam",
        cookingTime: 30,
        category: "Chicken",
        creatorName: "John Smith",
        creationDate: "15 Mar, 2024"
    },
    {
        id: 2,
        name: "Other green bullshit",
        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consectetur culpa dicta distinctio dolore eos error laboriosam modi quas quidem quisquam",
        cookingTime: 40,
        category: "Salad",
        creatorName: "Jane Doe",
        creationDate: "21 Apr, 2024"
    },
    {
        id: 3,
        name: "More green bullshit",
        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consectetur culpa dicta distinctio dolore eos error laboriosam modi quas quidem quisquam",
        cookingTime: 20,
        category: "Seafood",
        creatorName: "Bob Johnson",
        creationDate: "05 Jan, 2024"
    }
];

export default async function Home() {
    return (
        <main className="flex flex-col">
            <NavBar />
            <Container>
                <div className="mt-10">
                    <FeaturedRecipe {...featuredRecipes[0]}/>
                </div>
            </Container>
        </main>
    );
}
