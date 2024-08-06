import { Container, LinkButton } from "@/components";
import Image from "next/image";
import { CircleArrowRight } from "lucide-react";
import { FeaturedRecipeProps } from "@/schemas/featuredRecipe";


export default function FeaturedRecipe({
    name,
    description,
    timeToCook,
    category,
    creatorName,
    creationDate,
    imageData
}: FeaturedRecipeProps) {
    return (
        <Container className="h-full">
            <div
                className="flex flex-col-reverse lg:flex-row rounded-3xl bg-primary lg:max-h-[640px] h-full cursor-pointer">
                <div className="flex-1/2 flex flex-col justify-between gap-12 md:gap-24 py-6 md:py-12 px-4 sm:px-10">
                    <div
                        className="flex flex-col gap-6 md:gap-12 items-center text-center base:text-start base:items-start">
                        <div className="bg-white rounded-full py-3 px-5 font-medium shadow-lg flex items-center gap-3">
                            <Image src="/recipe-emoji.svg" alt="Recipe Icon" height={24} width={24} />
                            <p>Hot Recipes</p>
                        </div>
                        <div className="flex flex-col gap-6 md:gap-10">
                            <div
                                className="font-semibold text-4xl base:text-5xl md:text-[64px] leading-none">{name}</div>
                            <div className="leading-7 text-lg">{description}</div>
                            <div className="flex flex-col xs:flex-row justify-center base:justify-start gap-4">
                                <div
                                    className="bg-black bg-opacity-5 py-3 px-5 rounded-full flex gap-4 items-center justify-center xs:justify-start">
                                    <Image src="/timer.svg" alt="Timer" width={24} height={24} />
                                    {timeToCook} Minutes
                                </div>
                                <div
                                    className="bg-black bg-opacity-5 p-3 rounded-full flex gap-4 items-center justify-center xs:justify-start">
                                    <Image src="/fork-knife.svg" alt="Fork & Knife" width={24} height={24} />
                                    {category}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="flex flex-col gap-6 base:flex-row base:justify-between items-center w-full">
                        <div className="flex gap-3 items-center">
                            <Image src="/portrait.jpg" alt="Portrait" height={1000} width={1000}
                                   className="rounded-[100%] w-[50px] h-[50px]" />
                            <div className="flex flex-col gap-2">
                                <b className="text-lg">{creatorName}</b>
                                <p className="text-black text-opacity-60">{creationDate}</p>
                            </div>
                        </div>
                        <LinkButton url="#" buttonText="View Recipes"
                                    children={<CircleArrowRight color="#f8f1f1" size={20} />} />

                    </div>
                </div>
                <div className="h-full p-4 sm:p-6 base:p-8 md:p-10 lg:p-0 lg:flex-1/2">
                    <Image
                        src={imageData ? `data:image/jpeg;base64,${imageData}` : "/recipe-placeholder.avif"}
                        alt="Featured Recipe"
                        height={1500}
                        width={1500}
                        className="rounded-3xl lg:rounded-r-3xl lg:rounded-l-none h-full object-cover"
                    />
                </div>
            </div>
        </Container>
    );
}