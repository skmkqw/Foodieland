import { Container, FeaturedRecipe, NavBar } from "@/components";

export default async function Home() {
    return (
        <main className="flex flex-col">
            <NavBar />
            <Container>
                <div className="mt-4"></div>
                <FeaturedRecipe />
            </Container>
        </main>
    );
}
