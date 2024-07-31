import { CategoriesSection, FeaturedRecipeSkeleton, FeaturedSection, NavBar } from "@/components";
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
            </div>
        </main>
    );
}
