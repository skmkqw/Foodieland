import { Container, IngredientList } from "@/components";
import { Ingredient } from "@/types";

const ingredients: Ingredient[] = [
    { name: "Flour", amount: 2, unit: "cups" },
    { name: "Sugar", amount: 0.5, unit: "cups" },
    { name: "Butter", amount: 1, unit: "stick" },
    { name: "Eggs", amount: 3, unit: "pieces" },
    { name: "Vanilla Extract", amount: 1, unit: "teaspoon" }
];

export default function IngredientsSection() {
    return (
        <Container className="flex flex-col gap-5 md:gap-8 w-full mt-12 md:mt-20">
            <h1 className="font-semibold text-3xl md:text-4xl">Ingredients</h1>
            <IngredientList ingredients={ingredients} />
        </Container>
    );
}