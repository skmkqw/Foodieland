import { IngredientItem } from "@/components";
import { Ingredient } from "@/types";

export default function IngredientList({ ingredients }: { ingredients: Array<Ingredient> }) {
    return (
        <div className="flex flex-col w-full">
            {ingredients.map((item: Ingredient, idx) => (
                <IngredientItem key={idx} ingredient={item} />
            ))}
        </div>
    );
}