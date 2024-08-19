"use client";

import { Ingredient } from "@/types";
import { useState } from "react";
import { CheckButton } from "@/components";

export default function IngredientItem({ ingredient }: { ingredient: Ingredient }) {
    const [isChecked, setIsChecked] = useState(false);

    const handleCheckToggle = () => {
        setIsChecked(!isChecked);
    };

    return (
        <div className="w-full flex items-center justify-between py-6 md:py-8 border-b-2 border-b-black border-opacity-10">
            <div className="flex items-center gap-4">
                <CheckButton isChecked={isChecked} onToggle={handleCheckToggle} />
                <p className={`font-semibold text-xl md:text-2xl text-black ${isChecked ? "opacity-60 line-through" : "opacity-100 no-underline"}`}>{ingredient.name}</p>
            </div>
            <div className="flex items-center gap-2">
                <p className={`text-lg md:text-xl font-medium text-black ${isChecked ? "opacity-60" : "opacity-100"} transition-all duration-500`}>{ingredient.amount}</p>
                <p className={`text-lg md:text-xl font-medium text-black ${isChecked ? "opacity-60" : "opacity-100"} transition-all duration-500`}>{ingredient.unit}</p>
            </div>
        </div>
    );
}