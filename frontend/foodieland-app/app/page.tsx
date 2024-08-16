import {
    CategoriesSection,
    FeaturedRecipeSkeleton,
    FeaturedSection,
    Inbox,
    RecipeSectionSkeleton,
    RecipesSection
} from "@/components";
import React, { Suspense } from "react";


export default async function Home() {
    return (
        <main className="flex flex-col py-10 gap-40">
            <Suspense fallback={<FeaturedRecipeSkeleton />}>
                <FeaturedSection />
            </Suspense>
            <CategoriesSection />
            <Suspense fallback={<RecipeSectionSkeleton />}>
                <RecipesSection />
            </Suspense>
            <Inbox />
        </main>
    );
}
