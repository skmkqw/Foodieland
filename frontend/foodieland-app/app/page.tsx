import { FeaturedSlider, NavBar } from "@/components";


export default async function Home() {
    return (
        <main className="flex flex-col">
            <NavBar />
            <div className="mt-10 mb-20">
                <FeaturedSlider />
            </div>
        </main>
    );
}
