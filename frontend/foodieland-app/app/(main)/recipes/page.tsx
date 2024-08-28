import { Container, RecipeGridSkeleton, RecipesPageSection, Title } from "@/components";
import { Suspense } from "react";

export default function RecipesPage({ searchParams }) {
    return (
        <Container className="w-full py-10 flex flex-col items-center">
            <Title
                text="Explore simple and tasty recipes"
                className="text-4xl lg:text-start text-center"
            />
            <Suspense fallback={<RecipeGridSkeleton />}>
                <RecipesPageSection searchParams={searchParams} />
            </Suspense>
        </Container>
    );
}