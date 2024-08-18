"use client";

import { Check } from "lucide-react";

interface CheckButtonProps {
    isChecked: boolean;
    onToggle: (e: React.MouseEvent) => void;
    className?: string;
}

export default function CheckButton({ isChecked, onToggle, className }: CheckButtonProps) {
    return (
        <button
            onClick={onToggle}
            className={`w-[24px] h-[24px] cursor-pointer transition-all duration-500 flex items-center justify-center rounded-full ${isChecked ? "bg-black border-black" : "bg-white border-gray-400"} border-2 p-1 mt-1 ${className}`}
        >
            <Check
                size={24}
                color="#FFFFFF"
                strokeWidth={4}
            />
        </button>
    );
}