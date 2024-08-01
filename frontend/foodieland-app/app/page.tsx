import {
    CategoriesSection,
    FeaturedRecipeSkeleton,
    FeaturedSection,
    Footer,
    Inbox,
    NavBar,
    RecipesSection
} from "@/components";
import React, { Suspense } from "react";


export default async function Home() {
    return (
        <main className="flex flex-col">
            <NavBar />
            <div className="py-10 flex flex-col gap-40">
                <Suspense fallback={<FeaturedRecipeSkeleton />}>
                    <FeaturedSection />
                </Suspense>
                <CategoriesSection />
                <Suspense fallback={<p>Loading recipes..</p>} >
                    <RecipesSection />
                </Suspense>
                <Inbox />
            </div>
            <Footer />
        </main>
    );
}
