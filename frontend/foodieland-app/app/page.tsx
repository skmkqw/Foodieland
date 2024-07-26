import { Container, NavBar } from "@/components";

export default async function Home() {
    return (
        <main className="flex flex-col">
            <NavBar />
            <Container>
                <div></div>
            </Container>
        </main>
    );
}
