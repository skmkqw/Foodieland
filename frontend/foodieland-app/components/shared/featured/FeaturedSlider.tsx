"use client";

import { useEffect, useRef, useState } from "react";
import { FeaturedRecipeCard } from "@/components";
import { FeaturedRecipe } from "@/types";

export default function FeaturedSlider({ recipes }: { recipes: Array<FeaturedRecipe> }) {

    const [currentSlide, setCurrentSlide] = useState(0);
    const [isDragging, setIsDragging] = useState(false);
    const [startPos, setStartPos] = useState(0);
    const intervalRef = useRef<any>(null);

    const slideCount = recipes.length;

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
                {recipes.map((recipe, idx) => (
                    <div key={idx} className="flex-shrink-0 w-full">
                        <FeaturedRecipeCard recipe={recipe} />
                    </div>
                ))}
            </div>
        </div>
    );
}
