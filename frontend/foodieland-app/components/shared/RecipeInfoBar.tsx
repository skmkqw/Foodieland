import { CreatorInfo } from "@/components";
import Image from "next/image";

export default function RecipeInfoBar()  {
    return (
        <div className="grid grid-cols-3 sm:flex items-center gap-6">
            <CreatorInfo image={null} name="John Doe" creationDate="15 Mar, 2024" className="sm:pr-4 sm:flex-row flex-col" />
            <div className="flex sm:flex-row flex-col items-center gap-4 px-6 border-l-2 border-l-gray-400 border-r-2 border-r-gray-400">
                <Image src="/timer.svg" alt="Timer" width={24} height={24} />
                <div className="flex flex-col items-center sm:items-start text-center sm:text-left gap-2">
                    <p className="text-xs tracking-widest">COOKING TIME</p>
                    <p className="text-black opacity-60">15 Min</p>
                </div>
            </div>
            <div className="flex sm:flex-row flex-col items-center gap-4">
                <Image src="/fork-knife.svg" alt="fork and knife" width={24} height={24} />
                <div className="flex flex-col items-center sm:items-start gap-2">
                    <p className="text-xs tracking-widest">CATEGORY</p>
                    <p className="text-black opacity-60">Chicken</p>
                </div>
            </div>
        </div>
    );
}