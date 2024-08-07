import {
    CategoriesSection,
    FeaturedRecipeSkeleton,
    FeaturedSection,
    Footer,
    Header,
    Inbox,
    RecipeSectionSkeleton,
    RecipesSection
} from "@/components";
import React, { Suspense } from "react";


export default async function Home() {
    return (
        <main className="flex flex-col">
            <Header />
            <div className="py-10 flex flex-col gap-40">
                <Suspense fallback={<FeaturedRecipeSkeleton />}>
                    <FeaturedSection />
                </Suspense>
                <CategoriesSection />
                <Suspense fallback={<RecipeSectionSkeleton />}>
                    <RecipesSection />
                </Suspense>
                <Inbox />
            </div>
            <Footer />
        </main>
    );
}
