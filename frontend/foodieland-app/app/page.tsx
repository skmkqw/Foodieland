import { NavBar } from "@/components";
import FeaturedSection from "@/components/shared/FeaturedSection";


export default async function Home() {
    return (
        <main className="flex flex-col">
            <NavBar />
            <div className="mt-10 mb-20">
                <FeaturedSection />
            </div>
        </main>
    );
}
