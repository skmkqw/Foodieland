import { CategoryCard, Container, Title } from "@/components";

const CardsData = [
    {
        imagePath: "/breakfast.png",
        name: "Breakfast",
        gradientColors: "from-white to-lime-50",
    },
    {
        imagePath: "/vegan.png",
        name: "Vegan",
        gradientColors: "from-white to-green-50",
    },
    {
        imagePath: "/meat.png",
        name: "Meat",
        gradientColors: "from-white to-red-50",
    },
    {
        imagePath: "/dessert.png",
        name: "Dessert",
        gradientColors: "from-white to-amber-50",
    },
    {
        imagePath: "/lunch.png",
        name: "Lunch",
        gradientColors: "from-white to-gray-50",
    },
    {
        imagePath: "/chocolate.png",
        name: "Chocolate",
        gradientColors: "from-white to-gray-50",
    }
];

export default function CategoriesSection() {
    return (
        <Container className="flex flex-col gap-14 w-full">
            <Title text="Categories" className="text-center sm:text-start"/>
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-10">
                {CardsData.map((card, idx) => (
                    <CategoryCard {...card} key={idx} />
                ))}
            </div>
        </Container>
    );
}