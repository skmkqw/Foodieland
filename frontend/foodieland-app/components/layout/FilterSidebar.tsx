"use client";

import { ChevronDown, Filter } from "lucide-react";
import { CheckButton } from "@/components";
import { useState } from "react";

export default function FilterSidebar({ categories, className }: { categories: Array<string>, className?: string }) {
    const [isExpanded, setIsExpanded] = useState(true);
    const [timeRange, setTimeRange] = useState({ from: '', to: '' });
    const [selectedCategories, setSelectedCategories] = useState<Record<string, boolean>>({});

    const handleExpandToggle = () => setIsExpanded(prev => !prev);

    const handleTimeRangeChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setTimeRange(prev => ({ ...prev, [name]: value }));
    };

    const handleCategoryToggle = (category: string) => {
        setSelectedCategories(prev => ({
            ...prev,
            [category]: !prev[category]
        }));
    };

    return (
        <div
            className={`${className} flex flex-col border-primary border-4 rounded-3xl shadow-[0_3px_10px_rgb(0,0,0,0.2)] p-4 lg:p-6`}>
            <div
                className={`flex items-center justify-between ${isExpanded ? "pb-4" : "pb-0"} lg:pb-4 transition-all duration-500`}>
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
                            <p className="text-lg font-medium mt-1">{category}</p>
                        </div>
                    ))}
                </div>

                {/* Time Group */}
                <div className="flex flex-col py-6 border-t-2 border-t-gray-200">
                    <p className="text-xl font-semibold">Cooking Time</p>
                    <div className="flex flex-col gap-4 mt-4">
                        <div className="flex flex-col">
                            <label htmlFor="from" className="text-lg font-medium">From (min)</label>
                            <input
                                id="from"
                                name="from"
                                type="number"
                                value={timeRange.from}
                                onChange={handleTimeRangeChange}
                                placeholder="0"
                                className="border rounded-md p-2"
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
                                className="border rounded-md p-2"
                            />
                        </div>
                    </div>
                </div>
        </div>
    </div>);
}
