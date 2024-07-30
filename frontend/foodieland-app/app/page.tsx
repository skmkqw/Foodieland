import { NavBar } from "@/components";
import FeaturedSection from "@/components/shared/FeaturedSection";
import { Suspense } from "react";
import FeaturedRecipeSkeleton from "@/components/shared/FeaturedRecipeSkeleton";


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
