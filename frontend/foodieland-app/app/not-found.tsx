import { Container, Description, LinkButton, Title } from "@/components";

export default function NotFound() {
    return (
        <Container className="w-full py-44 flex flex-col items-center gap-8 text-center">
            <Title text="404" className="text-6xl md:text-8xl" />
            <Description text="Oops! It looks like you're looking for the page that doesn't exist." className="!text-2xl" />
            <LinkButton url="/" buttonText="Home" />
        </Container>
    );
}