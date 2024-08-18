import { Container, DirectionList, Title } from "@/components";
import { Direction } from "@/types";

export default function DirectionsSection({ directions }: { directions: Array<Direction> }) {
    return (
        <Container className="flex flex-col w-full mt-12 md:mt-20">
            <Title text="Directions" className="text-3xl md:text-4xl" />
            <DirectionList directions={directions} />
        </Container>
    );
}