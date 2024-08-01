import Image from "next/image";

export default function RecipeCard () {
    return (
        <div className="p-4 flex flex-col gap-7 bg-gradient-to-b from-white to-primary rounded-3xl">
            <Image src="/featured-recipe.jpg" alt="Recipe image" width={370} height={250} className="rounded-3xl" />
            <div className="flex flex-col gap-5 px-3">
                <p className="font-semibold text-3xl text-start">Lorem ipsum dolor sit dolor sit.</p>
                <div className="flex items-center gap-5 pb-9">
                    <div className="flex items-center gap-3">
                        <Image src="/timer.svg" alt="Fork and knife" width={24} height={24} />
                        <p className="text-base text-black text-opacity-60 font-medium">30 Minutes</p>
                    </div>
                    <div className="flex items-center gap-3">
                        <Image src="/fork-knife.svg" alt="Fork and knife" width={24} height={24} />
                        <p className="text-base text-black text-opacity-60 font-medium">Meat</p>
                    </div>
                </div>
            </div>
        </div>
    );
}