import { Container, RecipeCardSkeleton } from "@/components";

export default function RecipeGridSkeleton() {
    return (
        <Container className="w-full flex flex-col lg:grid lg:grid-cols-5 mt-10 gap-6">
            <div className="col-span-1 border-primary border-4 rounded-3xl shadow-[0_3px_10px_rgb(0,0,0,0.2)] p-4 lg:p-6"></div>
            <div className="col-span-4 grid grid-cols-1 md:grid-cols-2 gap-10">
                <RecipeCardSkeleton />
                <RecipeCardSkeleton />
                <RecipeCardSkeleton />
                <RecipeCardSkeleton />
            </div>
        </Container>
    );
}