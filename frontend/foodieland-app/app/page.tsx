import { FeaturedRecipeSkeleton, FeaturedSection, NavBar } from "@/components";
import React, { Suspense } from "react";


export default async function Home() {
    return (
        <main className="flex flex-col">
            <NavBar />
            <div className="mt-10 mb-20">
                <Suspense fallback={<FeaturedRecipeSkeleton />}>
                    <FeaturedSection />
                </Suspense>
            </div>
        </main>
    );
}
