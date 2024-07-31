"use client";

import { Button, CategoryCard, Container, Title } from "@/components";
import { useState } from "react";

const CardsData = [
    {
        imagePath: "/breakfast.png",
        name: "Breakfast",
        gradientColors: "from-white to-lime-50"
    },
    {
        imagePath: "/vegan.png",
        name: "Vegan",
        gradientColors: "from-white to-green-50"
    },
    {
        imagePath: "/meat.png",
        name: "Meat",
        gradientColors: "from-white to-red-50"
    },
    {
        imagePath: "/dessert.png",
        name: "Dessert",
        gradientColors: "from-white to-amber-50"
    },
    {
        imagePath: "/lunch.png",
        name: "Lunch",
        gradientColors: "from-white to-gray-50"
    },
    {
        imagePath: "/chocolate.png",
        name: "Chocolate",
        gradientColors: "from-white to-gray-50"
    },
    {
        imagePath: "/breakfast.png",
        name: "Breakfast",
        gradientColors: "from-white to-lime-50"
    },
    {
        imagePath: "/vegan.png",
        name: "Vegan",
        gradientColors: "from-white to-green-50"
    },
    {
        imagePath: "/meat.png",
        name: "Meat",
        gradientColors: "from-white to-red-50"
    },
    {
        imagePath: "/dessert.png",
        name: "Dessert",
        gradientColors: "from-white to-amber-50"
    },
    {
        imagePath: "/lunch.png",
        name: "Lunch",
        gradientColors: "from-white to-gray-50"
    },
    {
        imagePath: "/chocolate.png",
        name: "Chocolate",
        gradientColors: "from-white to-gray-50"
    }
];

export default function CategoriesSection() {
    const [showAll, setShowAll] = useState(false);

    const handleToggle = () => {
        setShowAll(prevShowAll => !prevShowAll);
    };

    const categoriesToShow = showAll ? CardsData : CardsData.slice(0, 6);

    return (
        <Container className="flex flex-col gap-14 w-full">
            <div className="flex flex-col items-center gap-10 md:flex-row justify-between text-center sm:text-start">
                <Title text="Categories" />
                <Button type="button" text={showAll ? "Show Less" : "View All Categories"}
                        additionalStyles="bg-primary !text-black hover:bg-gray-100" handleClick={handleToggle} />
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-10">
                {categoriesToShow.map((card, idx) => (
                    <CategoryCard {...card} key={idx} />
                ))}
            </div>
        </Container>
    );
}