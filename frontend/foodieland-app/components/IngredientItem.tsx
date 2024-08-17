"use client";

import { Ingredient } from "@/types";
import { Check } from "lucide-react";
import { useState } from "react";

export default function IngredientItem({ ingredient }: { ingredient: Ingredient }) {
    const [isChecked, setIsChecked] = useState(false);

    const handleCheckToggle = () => {
        setIsChecked(!isChecked);
    };

    return (
        <div className="w-full flex items-center justify-between py-6 md:py-8 border-b-2 border-b-black border-opacity-10">
            <div className="flex items-center gap-4">
                <Check
                    size={24}
                    color="#FFFFFF"
                    strokeWidth={4}
                    onClick={handleCheckToggle}
                    className={`cursor-pointer transition-all duration-500 flex items-center justify-center rounded-full ${isChecked ? "bg-black border-black" : "bg-white border-gray-400"} border-2 p-1`}
                />
                <p className={`font-semibold text-xl md:text-2xl text-black ${isChecked ? "opacity-60 line-through" : "opacity-100 no-underline"}`}>{ingredient.name}</p>
            </div>
            <div className="flex items-center gap-2">
                <p className={`text-lg md:text-xl font-medium text-black ${isChecked ? "opacity-60" : "opacity-100"} transition-all duration-500`}>{ingredient.amount}</p>
                <p className={`text-lg md:text-xl font-medium text-black ${isChecked ? "opacity-60" : "opacity-100"} transition-all duration-500`}>{ingredient.unit}</p>
            </div>
        </div>
    );
}