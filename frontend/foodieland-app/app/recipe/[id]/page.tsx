import { RecipeInfo } from "@/components";

const recipe = {
    name: "Health Japanese Fried Rice",
    description: "A quick and healthy Japanese-style fried rice with chicken, vegetables, and a perfect soft-boiled egg on top.",
    category: "Chicken",
    timeToCook: 32,
    creatorName: "Jane Smith",
    creationDate: "2022-03-15",
    userImage: null,
    imageData: null,
    nutritionInformation: {
        calories: 319.9,
        protein: 7.1,
        fat: 10.2,
        carbohydrate: 12.3,
        cholesterol: 32.4
    }
};
export default function RecipePage() {
    return (
        <main className="py-5 sm:py-8 md:py-10">
            <RecipeInfo {...recipe} />
        </main>
    );
}