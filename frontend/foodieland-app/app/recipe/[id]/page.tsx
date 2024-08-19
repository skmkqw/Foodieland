import { Inbox, RecipeContainer, RecipePageSkeleton } from "@/components";
import { Suspense } from "react";
import { notFound } from "next/navigation";

export default async function RecipePage({ params }) {
    if (!params.id) return notFound();

    return (
        <main className="flex flex-col py-5 sm:py-8 md:py-10">
            <Suspense fallback={<RecipePageSkeleton />}>
                <RecipeContainer id={params.id}/>
            </Suspense>
            <Inbox className="mt-12 md:mt-20 !px-6" />
        </main>
    );
}