import { Container, NutritionInformation, RecipeInfoBar } from "@/components";
import Image from "next/image";

export default function RecipeInfo() {
    return (
        <Container className="flex flex-col w-full gap-10 sm:gap-12 md:gap-16">
            <h1 className="text-4xl md:text-5xl lg:text-6xl font-semibold text-center sm:text-left">Healthy Japanese Fried Rice</h1>
            <RecipeInfoBar />
            <div className="grid grid-cols-1 md:grid-cols-3 w-full gap-10">
                <Image
                    src="/recipe-placeholder.avif"
                    alt="Featured Recipe"
                    height={600}
                    width={840}
                    className="rounded-3xl md:col-span-2"
                />
                <NutritionInformation className="md:col-span-1"/>
            </div>
        </Container>
    );
}