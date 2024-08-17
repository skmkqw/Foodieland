import Image from "next/image";
import { Container, NutritionInformationCard, RecipeInfoBar } from "@/components";
import { NutritionInformation, Recipe, RecipeCreator } from "@/types";

interface RecipeInfoProps {
    recipe: Recipe;
    nutritionInformation: NutritionInformation;
    creator: RecipeCreator
}

export default function RecipeInfo({ recipe, nutritionInformation, creator}: RecipeInfoProps) {
    return (
        <Container className="flex flex-col w-full gap-10 sm:gap-12 md:gap-16">
            <h1 className="text-4xl md:text-5xl lg:text-6xl font-semibold text-center sm:text-left">{recipe.name}</h1>
            <RecipeInfoBar
                category={recipe.category}
                timeToCook={recipe.timeToCook}
                creationDate={recipe.creationDate}
                creator={creator}
            />
            <div className="grid grid-cols-1 md:grid-cols-3 w-full gap-10">
                <Image
                    src="/recipe-placeholder.avif"
                    alt={recipe.imageData ? `data:image/jpeg;base64,${recipe.imageData}` : "/recipe-placeholder.avif"}
                    height={600}
                    width={840}
                    className="rounded-3xl md:col-span-2"
                />
                <NutritionInformationCard nutrition={nutritionInformation} />
            </div>
            <p className="font-medium text-xl text-black opacity-60 w-full">{recipe.description}</p>
        </Container>
    );
}
