import { Inbox, IngredientsSection, RecipeInfo } from "@/components";
import { RecipeExtended } from "@/types";

const recipeExtended: RecipeExtended = {
    recipe: {
        name: "Health Japanese Fried Rice",
        description: "A quick and healthy Japanese-style fried rice with chicken, vegetables, and a perfect soft-boiled egg on top.",
        category: "Chicken",
        timeToCook: 32,
        creationDate: "12 Apr, 2024",
        imageData: null
    },
    creator: {
        creatorName: "Jane Smith",
        userImage: null
    },
    nutritionInformation: {
        calories: 319.9,
        protein: 7.1,
        fat: 10.2,
        carbohydrate: 12.3,
        cholesterol: 32.4
    },
    ingredients: [
        { name: "Flour", amount: 2, unit: "cups" },
        { name: "Sugar", amount: 0.5, unit: "cups" },
        { name: "Butter", amount: 1, unit: "stick" },
        { name: "Eggs", amount: 3, unit: "pieces" },
        { name: "Vanilla", amount: 1, unit: "teaspoon" }
    ]
};
export default function RecipePage() {
    return (
        <main className="flex flex-col py-5 sm:py-8 md:py-10">
            <RecipeInfo
                recipe={recipeExtended.recipe}
                creator={recipeExtended.creator}
                nutritionInformation={recipeExtended.nutritionInformation}
            />
            <IngredientsSection ingredients={recipeExtended.ingredients} />
            <Inbox className="mt-12 md:mt-20"/>
        </main>
    );
}