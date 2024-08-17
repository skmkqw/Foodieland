import { Container, IngredientList } from "@/components";
import { Ingredient } from "@/types";

export default function IngredientsSection({ ingredients }: { ingredients: Array<Ingredient> }) {
    return (
        <Container className="flex flex-col gap-5 md:gap-8 w-full mt-12 md:mt-20">
            <h1 className="font-semibold text-3xl md:text-4xl">Ingredients</h1>
            <IngredientList ingredients={ingredients} />
        </Container>
    );
}