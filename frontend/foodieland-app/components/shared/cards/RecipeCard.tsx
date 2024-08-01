import Image from "next/image";

interface RecipeCardProps {
    imagePath: string,
    name: string,
    category: string,
    timeToCook: number
}

export default function RecipeCard({ imagePath, name, category, timeToCook }: RecipeCardProps) {
    return (
        <div className="p-4 flex flex-col gap-7 bg-gradient-to-b from-white to-primary rounded-3xl">
            <Image src={imagePath} alt="Recipe image" width={370} height={250} className="rounded-3xl w-full object-cover" />
            <div className="flex flex-col gap-5 px-3">
                <p className="font-semibold text-3xl text-start">{name}</p>
                <div className="flex items-center gap-5 pb-9">
                    <div className="flex items-center gap-3">
                        <Image src="/timer.svg" alt="Fork and knife" width={24} height={24} />
                        <p className="text-base text-black text-opacity-60 font-medium">{timeToCook} Minutes</p>
                    </div>
                    <div className="flex items-center gap-3">
                        <Image src="/fork-knife.svg" alt="Fork and knife" width={24} height={24} />
                        <p className="text-base text-black text-opacity-60 font-medium">{category}</p>
                    </div>
                </div>
            </div>
        </div>
    );
}