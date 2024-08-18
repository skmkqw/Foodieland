"use client";

import { Direction } from "@/types";
import { useState } from "react";
import { CheckButton, Description } from "@/components";

export default function DirectionItem({ direction }: { direction: Direction }) {

    const [isChecked, setIsChecked] = useState(false);

    const handleCheckToggle = () => {
        setIsChecked(!isChecked);
    };

    return (
        <div className="flex gap-4 sm:gap-5 py-8 md:py-12 border-b-2 border-b-black border-opacity-10">
            <CheckButton isChecked={isChecked} onToggle={handleCheckToggle} />
            <div className="flex flex-col gap-4">
                <p className={`font-semibold text-xl md:text-2xl text-black ${isChecked ? "opacity-60 line-through" : "opacity-100 no-underline"}`}>{direction.stepNumber}. {direction.name}</p>
                <Description text={direction.description} className="" />
            </div>
        </div>
    );
}