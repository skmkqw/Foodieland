import { Direction } from "@/types";
import { DirectionItem } from "@/components";

export default function DirectionList({ directions }: { directions: Array<Direction> }) {
    return (
        <div className="flex flex-col w-full">
            {directions.map((direction, idx) => (
                <DirectionItem direction={direction} key={idx} />
            ))}
        </div>
    );
}