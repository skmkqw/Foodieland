"use client";

import { Error, RecipeCard } from "@/components";
import { RecipeShort } from "@/types";
import { useEffect, useState } from "react";
import { useSearchParams } from "next/navigation";

export default function RecipeGrid({ recipes }: { recipes: Array<RecipeShort> }) {
    const [filteredRecipes, setFilteredRecipes] = useState<Array<RecipeShort>>(recipes);

    const searchParams = useSearchParams()

    useEffect(() => {
        const categories = searchParams.getAll("categories");
        const from = Number(searchParams.get("from")) || 0;
        const to = Number(searchParams.get("to")) || Infinity;

        const filtered = recipes?.filter(recipe => {
            const isCategoryMatch = categories.length === 0 || categories.includes(recipe.category);
            const isTimeMatch = (recipe.timeToCook >= from && recipe.timeToCook <= to);
            return isCategoryMatch && isTimeMatch;
        }) || recipes;

        setFilteredRecipes(filtered);
    }, [searchParams, recipes]);

    if (filteredRecipes.length === 0) return <Error errorMessage="No recipes found!" className="col-span-4" />;

    return (
        <div className="col-span-4 grid grid-cols-1 md:grid-cols-2 gap-10">
            {filteredRecipes.map((recipe, idx) => (
                <RecipeCard recipe={recipe} key={idx} />
            ))}
        </div>
    );
}