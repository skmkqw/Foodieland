"use client";

import { ChevronDown, Filter } from "lucide-react";
import { FilterGroup } from "@/components";
import { FilterGroupProps } from "@/types";
import { useState } from "react";

export default function FilterSidebar({ groups, className }: { groups: Array<FilterGroupProps>, className?: string }) {
    const [isExpanded, setIsExpanded] = useState(true);

    const handleExpandToggle = () => setIsExpanded((prev) => !prev);

    return (
        <div className={`${className} flex flex-col border-primary border-4 rounded-3xl shadow-[0_3px_10px_rgb(0,0,0,0.2)] p-4 lg:p-6`}>
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
                {groups.map((item, idx) => (
                    <FilterGroup groupName={item.groupName} filterNames={item.filterNames} key={idx} />
                ))}
            </div>
        </div>
    );
}