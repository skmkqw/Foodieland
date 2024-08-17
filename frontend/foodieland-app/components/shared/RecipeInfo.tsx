import Image from "next/image";
import { Container, NutritionInformation, RecipeInfoBar } from "@/components";
import { NutritionInformationProps } from "@/components/shared/NutritionInformation";

interface RecipeInfoProps {
    name: string;
    description: string;
    category: string;
    timeToCook: number;
    creatorName: string;
    creationDate: string;
    userImage: string | null;
    imageData: string | null;
    nutritionInformation: NutritionInformationProps;
}

export default function RecipeInfo({
    name,
    description,
    category,
    timeToCook,
    creatorName,
    creationDate,
    userImage,
    imageData,
    nutritionInformation
}: RecipeInfoProps) {
    return (
        <Container className="flex flex-col w-full gap-10 sm:gap-12 md:gap-16">
            <h1 className="text-4xl md:text-5xl lg:text-6xl font-semibold text-center sm:text-left">{name}</h1>
            <RecipeInfoBar
                category={category}
                timeToCook={timeToCook}
                creatorName={creatorName}
                creationDate={creationDate}
                userImage={userImage}
            />
            <div className="grid grid-cols-1 md:grid-cols-3 w-full gap-10">
                <Image
                    src="/recipe-placeholder.avif"
                    alt={imageData ? `data:image/jpeg;base64,${imageData}` : "/recipe-placeholder.avif"}
                    height={600}
                    width={840}
                    className="rounded-3xl md:col-span-2"
                />
                <NutritionInformation {...nutritionInformation} />
            </div>
            <p className="font-medium text-xl text-black opacity-60 w-full">{description}</p>
        </Container>
    );
}
