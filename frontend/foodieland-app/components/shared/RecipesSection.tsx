import { Container, Description, RecipeCard, Title } from "@/components";
import { fetchRecipes } from "@/actions/recipes";
import React from "react";
import { RecipeShort } from "@/types";


export default async function RecipesSection() {
    const recipes: Array<RecipeShort> | undefined = await fetchRecipes(6);

    if (!recipes) return (
        <Container>
            <div className="rounded-3xl bg-primary flex items-center justify-center p-10 text-center">
                <p className="font-medium text-2xl">Oops! An unexpected error occurred while fetching recipes.
                    Please try again later.</p>
            </div>
        </Container>
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