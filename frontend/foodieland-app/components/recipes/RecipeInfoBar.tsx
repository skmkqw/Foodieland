"use client";

import { CreatorInfo, LikeButton } from "@/components";
import Image from "next/image";
import { RecipeCreator } from "@/types";
import { likeRecipe, unlikeRecipe } from "@/actions/recipes";
import { toast } from "sonner";
import { useState } from "react";

interface RecipeInfoBarProps {
    id: string;
    timeToCook: number,
    category: string,
    creationDate: string,
    isLiked: boolean,
    creator: RecipeCreator
}

export default function RecipeInfoBar({
    id,
    timeToCook,
    category,
    creationDate,
    creator,
    isLiked
}: RecipeInfoBarProps) {

    const [likeButtonActive, setLikeButtonActive] = useState(isLiked);

    const handleToggleLike = async (e: React.MouseEvent) => {
        const action = likeButtonActive ? unlikeRecipe : likeRecipe;
        const success = await action(id);
        if (success) {
            setLikeButtonActive(!likeButtonActive);
            toast.success(likeButtonActive ? "Recipe unliked!" : "Recipe liked!");
        } else toast.error("Please log in before liking recipes");
    };

    return (
        <div className="flex flex-col md:flex-row items-center gap-x-4 gap-y-10 md:gap-6">
            <CreatorInfo
                creator={creator}
                creationDate={creationDate}
            />
            <div className="flex items-center gap-6">
                <div className="flex gap-4 pr-4 md:px-6 md:border-l-2 md:border-l-gray-400 border-r-2 border-r-gray-400">
                    <Image src="/timer.svg" alt="Timer" width={24} height={24} />
                    <div className="flex flex-col items-center md:items-start text-center md:text-left gap-2">
                        <p className="text-xs tracking-widest">COOKING TIME</p>
                        <p className="text-black opacity-60">{timeToCook} Min</p>
                    </div>
                </div>
                <div className="flex gap-4 pr-4 md:pr-6 border-r-2 border-r-gray-400">
                    <Image src="/fork-knife.svg" alt="fork and knife" width={24} height={24} />
                    <div className="flex flex-col items-center md:items-start gap-2">
                        <p className="text-xs tracking-widest">CATEGORY</p>
                        <p className="text-black opacity-60">{category}</p>
                    </div>
                </div>
                <LikeButton isLiked={likeButtonActive} onToggle={handleToggleLike} />
            </div>
        </div>
    );
}