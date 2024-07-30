"use client";

import { useEffect, useRef, useState } from "react";
import { FeaturedRecipe } from "@/components";

const recipeData = [
    {
        id: 1,
        name: "Some green bullshit",
        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consectetur culpa dicta distinctio dolore eos error laboriosam modi quas quidem quisquam",
        cookingTime: 30,
        category: "Chicken",
        creatorName: "John Smith",
        creationDate: "15 Mar, 2024"
    },
    {
        id: 2,
        name: "Other green bullshit",
        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consectetur culpa dicta distinctio dolore eos error laboriosam modi quas quidem quisquam",
        cookingTime: 40,
        category: "Salad",
        creatorName: "Jane Doe",
        creationDate: "21 Apr, 2024"
    },
    {
        id: 3,
        name: "More green bullshit",
        description: "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consectetur culpa dicta distinctio dolore eos error laboriosam modi quas quidem quisquam",
        cookingTime: 20,
        category: "Seafood",
        creatorName: "Bob Johnson",
        creationDate: "05 Jan, 2024"
    }
];

export default function FeaturedSlider() {

    const [currentSlide, setCurrentSlide] = useState(0);
    const [isDragging, setIsDragging] = useState(false);
    const [startPos, setStartPos] = useState(0);
    const intervalRef = useRef(null);

    const slideCount = recipeData.length;

    const startAutoplay = () => {
        intervalRef.current = setInterval(() => {
            setCurrentSlide((prev) => (prev + 1) % slideCount);
        }, 5000);
    };

    const resetAutoplay = () => {
        if (intervalRef.current) {
            clearInterval(intervalRef.current);
            startAutoplay();
        }
    };

    useEffect(() => {
        startAutoplay();
        return () => clearInterval(intervalRef.current);
    }, []);

    const handleDragStart = (clientX) => {
        setIsDragging(true);
        setStartPos(clientX);
        resetAutoplay();
    };

    const handleDragMove = (clientX) => {
        if (!isDragging) return;
        if (startPos - clientX > 50) {
            setCurrentSlide((prev) => (prev + 1) % slideCount);
            setIsDragging(false);
        } else if (startPos - clientX < -50) {
            setCurrentSlide((prev) => (prev - 1 + slideCount) % slideCount);
            setIsDragging(false);
        }
    };

    const handleDragEnd = () => {
        setIsDragging(false);
    };

    const handleTouchStart = (e) => handleDragStart(e.touches[0].clientX);
    const handleTouchMove = (e) => handleDragMove(e.touches[0].clientX);
    const handleTouchEnd = handleDragEnd;

    const handleMouseDown = (e) => handleDragStart(e.clientX);
    const handleMouseMove = (e) => {
        if (isDragging) handleDragMove(e.clientX);
    };

    return (
        <div
            className="overflow-hidden rounded-3xl"
            onTouchStart={handleTouchStart}
            onTouchMove={handleTouchMove}
            onTouchEnd={handleTouchEnd}
            onMouseDown={handleMouseDown}
            onMouseMove={handleMouseMove}
            onMouseUp={handleDragEnd}
            onMouseLeave={handleDragEnd}
        >
            <div
                className="flex items-stretch transition-transform duration-700 "
                style={{ transform: `translateX(-${currentSlide * 100}%)` }}
            >
                {recipeData.map((recipe) => (
                    <div key={recipe.id} className="flex-shrink-0 w-full">
                        <FeaturedRecipe {...recipe} />
                    </div>
                ))}
            </div>
        </div>
    );
}
