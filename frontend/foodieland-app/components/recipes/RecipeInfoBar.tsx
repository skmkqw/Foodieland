import { CreatorInfo } from "@/components";
import Image from "next/image";
import { RecipeCreator } from "@/types";

interface RecipeInfoBarProps {
    timeToCook: number,
    category: string,
    creationDate: string,
    creator: RecipeCreator
}

export default function RecipeInfoBar({
    timeToCook,
    category,
    creationDate,
    creator
}: RecipeInfoBarProps) {
    return (
        <div className="flex flex-col sm:flex-row items-center gap-x-4 gap-y-10 md:gap-6">
            <CreatorInfo
                creator={creator}
                creationDate={creationDate}
            />
            <div className="flex items-center gap-6">
                <div className="flex gap-4 pr-4 sm:px-6 sm:border-l-2 sm:border-l-gray-400 border-r-2 border-r-gray-400">
                    <Image src="/timer.svg" alt="Timer" width={24} height={24} />
                    <div className="flex flex-col items-center sm:items-start text-center sm:text-left gap-2">
                        <p className="text-xs tracking-widest">COOKING TIME</p>
                        <p className="text-black opacity-60">{timeToCook} Min</p>
                    </div>
                </div>
                <div className="flex gap-4">
                    <Image src="/fork-knife.svg" alt="fork and knife" width={24} height={24} />
                    <div className="flex flex-col items-center sm:items-start gap-2">
                        <p className="text-xs tracking-widest">CATEGORY</p>
                        <p className="text-black opacity-60">{category}</p>
                    </div>
                </div>
            </div>
        </div>
    );
}