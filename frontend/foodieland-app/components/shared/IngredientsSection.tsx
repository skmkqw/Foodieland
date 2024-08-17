import { Container, IngredientList, Title } from "@/components";
import { Ingredient } from "@/types";

export default function IngredientsSection({ ingredients }: { ingredients: Array<Ingredient> }) {
    return (
        <Container className="flex flex-col gap-5 md:gap-8 w-full mt-12 md:mt-20">
            <Title text="Ingredients" className="text-3xl md:text-4xl" />
            <IngredientList ingredients={ingredients} />
        </Container>
    );
}