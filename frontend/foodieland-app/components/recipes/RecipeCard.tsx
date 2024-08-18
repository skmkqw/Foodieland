"use client";

import Image from "next/image";
import { LikeButton } from "@/components";
import { useState } from "react";
import { likeRecipe, unlikeRecipe } from "@/actions/recipes";
import { toast } from "sonner";
import Link from "next/link";
import { RecipeShort } from "@/types";

export default function RecipeCard({ recipe }: { recipe: RecipeShort }) {
    const [likeButtonActive, setLikeButtonActive] = useState(recipe.isLiked);

    const handleToggleLike = async (e: React.MouseEvent) => {
        e.preventDefault();
        const action = likeButtonActive ? unlikeRecipe : likeRecipe;
        const success = await action(recipe.id);
        if (success) {
            setLikeButtonActive(!likeButtonActive);
            toast.success(likeButtonActive ? "Recipe unliked!" : "Recipe liked!");
        } else toast.error("Please log in before liking recipes");
    };

    return (
        <Link href={`/recipe/${recipe.id}`} passHref>
            <div className="relative p-4 flex flex-col gap-7 bg-gradient-to-b from-white to-primary rounded-3xl cursor-pointer">
                <LikeButton
                    isLiked={likeButtonActive}
                    onToggle={handleToggleLike}
                    className="absolute top-[6%] right-[10%]"
                />
                <div className="relative w-full h-64">
                    <Image
                        src={recipe.imageData ? `data:image/jpeg;base64,${recipe.imageData}` : "/recipe-placeholder.avif"}
                        alt="Recipe image"
                        width={400}
                        height={250}
                        className="rounded-3xl object-cover h-full"
                    />
                </div>
                <div className="flex flex-col gap-5 px-3">
                    <p className="font-semibold text-3xl text-start">{recipe.name}</p>
                    <div className="flex items-center gap-5 pb-9">
                        <div className="flex items-center gap-3">
                            <Image src="/timer.svg" alt="Timer" width={24} height={24} />
                            <p className="text-base text-black text-opacity-60 font-medium">{recipe.timeToCook} Minutes</p>
                        </div>
                        <div className="flex items-center gap-3">
                            <Image src="/fork-knife.svg" alt="Fork and knife" width={24} height={24} />
                            <p className="text-base text-black text-opacity-60 font-medium">{recipe.category}</p>
                        </div>
                    </div>
                </div>
            </div>
        </Link>
    );
}