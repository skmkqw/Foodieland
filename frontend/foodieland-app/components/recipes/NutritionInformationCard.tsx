import { NutritionInformation } from "@/types";

function NutritionCategory({ category, value, unit }: { category: string, value: number, unit: string }) {
    return (
        <div className="flex w-full justify-between items-center border-b-2 border-gray-400 py-5 gap-4">
            <p className="font-medium text-black opacity-60 text-lg">{category}</p>
            <div className="font-medium text-lg flex items-center gap-1">
                <p>{value}</p>
                <p>{unit}</p>
            </div>
        </div>
    );
}

interface NutritionInformationProps {
    className?: string,
    nutrition: NutritionInformation
}

export function NutritionInformationCard({ className, nutrition }: NutritionInformationProps) {
    const categoryToValue = [
        {
            category: "Calories",
            value: nutrition.calories,
            unit: "kcal"
        },
        {
            category: "Total Fat",
            value: nutrition.protein,
            unit: "g"
        },
        {
            category: "Protein",
            value: nutrition.fat,
            unit: "g"
        },
        {
            category: "Carbohydrate",
            value: nutrition.carbohydrate,
            unit: "g"
        },
        {
            category: "Cholesterol",
            value: nutrition.cholesterol,
            unit: "mg"
        }
    ];
    return (
        <div className={`${className} w-full rounded-3xl bg-primary p-9`}>
            <p className="text-2xl font-semibold">Nutrition Information</p>
            {categoryToValue.map((item) => (
                <NutritionCategory category={item.category} value={item.value} unit={item.unit} />
            ))}
        </div>
    );
}