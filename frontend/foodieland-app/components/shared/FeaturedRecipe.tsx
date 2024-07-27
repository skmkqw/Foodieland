import { Container, LinkButton } from "@/components";
import Image from "next/image";

export default function FeaturedRecipe() {
    return (
        <Container>
            <div className="flex flex-col-reverse lg:flex-row rounded-3xl bg-primary lg:max-h-[640px]">
                <div className="flex-1/2 flex flex-col justify-between gap-12 md:gap-24 py-6 md:py-12 px-4 sm:px-10">
                    <div className="flex flex-col gap-6 md:gap-12 items-center text-center base:text-start base:items-start">
                        <div className="bg-white rounded-full py-3 px-5 font-medium shadow-lg flex items-center gap-3">
                            <Image src="/recipe-emoji.svg" alt="Recipe Icon" height={24} width={24} />
                            <p>Hot Recipes</p>
                        </div>
                        <div className="flex flex-col gap-6 md:gap-10">
                            <div className="font-semibold text-4xl base:text-5xl md:text-[64px] leading-none">Some green bullshit</div>
                            <div className="leading-7 text-lg">Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                Consectetur culpa dicta
                                distinctio dolore eos error laboriosam modi quas quidem quisquam.
                            </div>
                            <div className="flex flex-col xs:flex-row justify-center base:justify-start gap-4">
                                <div className="bg-black bg-opacity-5 py-3 px-5 rounded-full flex gap-4 items-center justify-center xs:justify-start">
                                    <Image src="/timer.svg" alt="Timer" width={24} height={24} />
                                    30 Minutes
                                </div>
                                <div className="bg-black bg-opacity-5 p-3 rounded-full flex gap-4 items-center justify-center xs:justify-start">
                                    <Image src="/fork-knife.svg" alt="Fork & Knife" width={24} height={24} />
                                    Chicken
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="flex flex-col gap-6 base:flex-row base:justify-between items-center w-full">
                        <div className="flex gap-3 items-center">
                            <Image src="/portrait.jpg" alt="Portrait" height={1000} width={1000}
                                   className="rounded-[100%] w-[50px] h-[50px]" />
                            <div className="flex flex-col gap-2">
                                <b className="text-lg">John Smith</b>
                                <p className="text-black text-opacity-60">15 March 2022</p>
                            </div>
                        </div>
                        <LinkButton url="#" buttonText="View Recipes" />
                    </div>
                </div>
                <div className="flex-1/2">
                    <Image src="/featured-recipe.jpg" alt="Featured Recipe" height={1500} width={1500}
                           className="rounded-3xl lg:rounded-r-3xl lg:rounded-l-none h-full object-cover" />
                </div>
            </div>
        </Container>
    );
}