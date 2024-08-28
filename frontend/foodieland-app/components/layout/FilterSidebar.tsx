"use client";

import { ChevronDown, Filter } from "lucide-react";
import { Button, CheckButton } from "@/components";
import { useEffect, useState } from "react";
import { useRouter, useSearchParams } from "next/navigation";

export default function FilterSidebar({ categories, className }: { categories: Array<string>, className?: string }) {
    const [isExpanded, setIsExpanded] = useState(true);
    const [timeRange, setTimeRange] = useState({ from: 1, to: 60 });
    const [selectedCategories, setSelectedCategories] = useState<Record<string, boolean>>({});

    const [categoryFilterActive, setCategoryFilterActive] = useState(false);
    const [timeRangeFilterActive, setTimeRangeFilterActive] = useState(false);

    const router = useRouter();
    const searchParams = useSearchParams();

    useEffect(() => {
        if (!categoryFilterActive && !timeRangeFilterActive) return;
        const query = new URLSearchParams(searchParams.toString());

        query.set("page", "1");

        categoryFilterActive && Object.keys(selectedCategories).forEach(category => {
            if (selectedCategories[category]) {
                query.append("categories", category);
            }
        });

        if (timeRangeFilterActive) {
            if (timeRange.from) query.set("from", timeRange.from.toString());
            if (timeRange.to) query.set("to", timeRange.to.toString());
        }

        router.push(`?${query.toString()}`);
    }, [selectedCategories, timeRange, router]);

    const handleExpandToggle = () => setIsExpanded(prev => !prev);

    const handleTimeRangeChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setTimeRangeFilterActive(true);
        const { name, value } = e.target;
        setTimeRange(prev => ({ ...prev, [name]: value }));
    };

    const handleCategoryToggle = (category: string) => {
        setCategoryFilterActive(true);
        setSelectedCategories(prev => ({
            ...prev,
            [category]: !prev[category]
        }));
    };

    const clearFilters = () => {
        setTimeRangeFilterActive(false);
        setCategoryFilterActive(false);
        Object.keys(selectedCategories).forEach(category => {
            setSelectedCategories(prev => ({ ...prev, [category]: false }));
        });
        router.push("/recipes/favourite");
    }

    return (
        <div
            className={`${className} flex flex-col border-primary border-4 rounded-3xl shadow-[0_3px_10px_rgb(0,0,0,0.2)] p-4 lg:p-6`}>
            <div className={`flex items-center justify-between ${isExpanded ? "pb-4" : "pb-0"} lg:pb-4 transition-all duration-500`}>
                <div className="flex items-center gap-8">
                    <Filter />
                    <p className="text-2xl font-semibold">Filters</p>
                </div>
                <ChevronDown
                    className={`${isExpanded ? "rotate-180" : "rotate-0"} block lg:hidden transition-all duration-500 cursor-pointer`}
                    onClick={handleExpandToggle}
                />
            </div>
            <div className={`transition-[max-height] duration-500 ease-in-out overflow-hidden ${isExpanded ? "max-h-[1000px]" : "max-h-0"} lg:max-h-none`}>
                {/* Categories Group */}
                <div className="flex flex-col py-6 border-t-2 border-t-gray-200">
                    <p className="text-xl font-semibold">Categories</p>
                    {categories.map((category, idx) => (
                        <div key={idx} className="flex items-center gap-4 mt-4">
                            <CheckButton
                                isChecked={selectedCategories[category]}
                                onToggle={() => handleCategoryToggle(category)}
                            />
                            <p className={`${categoryFilterActive ? "text-black" : "text-gray-400"} text-lg font-medium mt-1 transition-all duration-500`}>{category}</p>
                        </div>
                    ))}
                </div>

                {/* Time Group */}
                <div className="flex flex-col py-6 border-t-2 border-t-gray-200">
                    <p className="text-xl font-semibold">Cooking Time</p>
                    <div className={`${timeRangeFilterActive ? "text-black" : "text-gray-400"} flex flex-col gap-4 mt-4 transition-all duration-500`}>
                        <div className="flex flex-col">
                            <label htmlFor="from" className="text-lg font-medium">From (min)</label>
                            <input
                                id="from"
                                name="from"
                                type="number"
                                value={timeRange.from}
                                onChange={handleTimeRangeChange}
                                placeholder="0"
                                className="border rounded-xl p-2"
                            />
                        </div>
                        <div className="flex flex-col">
                            <label htmlFor="to" className="text-lg font-medium">To (min)</label>
                            <input
                                id="to"
                                name="to"
                                type="number"
                                value={timeRange.to}
                                onChange={handleTimeRangeChange}
                                placeholder="100"
                                className="border rounded-xl p-2"
                            />
                        </div>
                    </div>
                </div>
                <Button type="button" text="Clear Filters" handleClick={clearFilters}/>
            </div>
        </div>);
}
