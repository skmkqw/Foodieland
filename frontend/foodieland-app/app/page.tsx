import { Container, FeaturedRecipe, NavBar } from "@/components";

export default async function Home() {
    return (
        <main className="flex flex-col">
            <NavBar />
            <Container>
                <div className="py-10">
                    <FeaturedRecipe />
                </div>
            </Container>
        </main>
    );
}
