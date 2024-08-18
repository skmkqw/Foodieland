"use client";

import { Direction } from "@/types";
import { Check } from "lucide-react";
import { useState } from "react";
import { Description } from "@/components";

export default function DirectionItem({ direction }: { direction: Direction }) {

    const [isChecked, setIsChecked] = useState(false);

    const handleCheckToggle = () => {
        setIsChecked(!isChecked);
    };

    return (
        <div className="flex gap-4 sm:gap-5 py-8 md:py-12 border-b-2 border-b-black border-opacity-10">
            <div
                onClick={handleCheckToggle}
                className={`w-[24px] h-[24px] cursor-pointer transition-all duration-500 flex items-center justify-center rounded-full ${isChecked ? "bg-black border-black" : "bg-white border-gray-400"} border-2 p-1 mt-1`}
            >
                <Check
                    size={24}
                    color="#FFFFFF"
                    strokeWidth={4}
                />
            </div>
            <div className="flex flex-col gap-4">
                <p className={`font-semibold text-xl md:text-2xl text-black ${isChecked ? "opacity-60 line-through" : "opacity-100 no-underline"}`}>{direction.stepNumber}. {direction.name}</p>
                <Description text={direction.description} className="" />
            </div>
        </div>
    );
}