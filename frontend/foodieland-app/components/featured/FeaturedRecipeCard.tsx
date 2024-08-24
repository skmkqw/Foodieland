import { Container, CreatorInfo, LinkButton } from "@/components";
import Image from "next/image";
import { CircleArrowRight } from "lucide-react";
import { FeaturedRecipe } from "@/types";

export default function FeaturedRecipeCard({ featuredRecipe }: { featuredRecipe: FeaturedRecipe }) {
    return (
        <Container className="h-full">
            <div
                className="flex flex-col-reverse lg:flex-row rounded-3xl bg-primary lg:max-h-[640px] h-full cursor-pointer">
                <div
                    className="flex-1/2 flex flex-col justify-between gap-12 md:gap-24 py-6 md:py-12 px-4 sm:px-10">
                    <div
                        className="flex flex-col gap-6 md:gap-12 items-center text-center base:text-start base:items-start">
                        <div
                            className="bg-white rounded-full py-3 px-5 font-medium shadow-lg flex items-center gap-3">
                            <Image src="/recipe-emoji.svg" alt="Recipe Icon" height={24} width={24} />
                            <p>Hot Recipes</p>
                        </div>
                        <div className="flex flex-col gap-6 md:gap-10">
                            <div
                                className="font-semibold text-4xl base:text-5xl md:text-[64px] leading-none">{featuredRecipe.recipe.name}</div>
                            <div className="leading-7 text-lg">{featuredRecipe.recipe.description}</div>
                            <div className="flex flex-col xs:flex-row justify-center base:justify-start gap-4">
                                <div
                                    className="bg-black bg-opacity-5 py-3 px-5 rounded-full flex gap-4 items-center justify-center xs:justify-start">
                                    <Image src="/timer.svg" alt="Timer" width={24} height={24} />
                                    {featuredRecipe.recipe.timeToCook} Minutes
                                </div>
                                <div
                                    className="bg-black bg-opacity-5 p-3 rounded-full flex gap-4 items-center justify-center xs:justify-start">
                                    <Image src="/fork-knife.svg" alt="Fork & Knife" width={24} height={24} />
                                    {featuredRecipe.recipe.category}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="flex flex-col gap-6 base:flex-row base:justify-between items-center w-full">
                        <CreatorInfo creationDate={featuredRecipe.recipe.creationDate} creator={featuredRecipe.creator} />
                        <LinkButton
                            url={`/recipes/${featuredRecipe.recipe.id}`}
                            buttonText="View Recipe"
                            children={<CircleArrowRight color="#f8f1f1" size={20} />}
                        />

                    </div>
                </div>
                <div className="h-full p-4 sm:p-6 base:p-8 md:p-10 lg:p-0 lg:flex-1/2">
                    <Image
                        src={featuredRecipe.recipe.imageData ? `data:image/jpeg;base64,${featuredRecipe.recipe.imageData}` : "/recipe-placeholder.avif"}
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