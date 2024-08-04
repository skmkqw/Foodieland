import { Container, Description, RecipeCardSkeleton, Title } from "@/components";

export default function RecipeSectionSkeleton() {
    return (
        <Container className="w-full flex flex-col items-center gap-10 text-center">
            <Title text="Simple and tasty recipes" />
            <Description
                text="Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ad aut enim libero neque non quas repellat repellendus tenetur ullam voluptates?"
                className="max-w-2xl" />
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-10">
                <RecipeCardSkeleton />
                <RecipeCardSkeleton />
                <RecipeCardSkeleton />
            </div>
        </Container>
    );
}