import { Container, IngredientList } from "@/components";

export default function IngredientsSection() {
    return (
        <Container className="flex flex-col gap-20 w-full mt-20">
            <h1 className="font-semibold text-4xl">Ingredients</h1>
            <IngredientList />
        </Container>
    );
}