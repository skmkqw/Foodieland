import { Container, FavouriteSection, RecipeGridSkeleton, Title } from "@/components";
import { Suspense } from "react";

export default async function FavouritePage({ searchParams }) {
    return (
        <Container className="w-full py-10 flex flex-col items-center">
            <Title
                text="Recipes that bring happiness to your table ❤️"
                className="text-4xl lg:text-start text-center"
            />
            <Suspense fallback={<RecipeGridSkeleton />}>
                <FavouriteSection searchParams={searchParams} />
            </Suspense>
        </Container>
    );
}