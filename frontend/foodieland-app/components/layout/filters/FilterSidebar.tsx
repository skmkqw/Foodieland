import { Filter } from "lucide-react";
import { FilterGroup } from "@/components";
import { FilterGroupProps } from "@/types";

export default function FilterSidebar({ groups, className }: { groups: Array<FilterGroupProps>, className?: string }) {
    return (
        <div className={`${className} flex flex-col border-primary border-4 rounded-3xl shadow-[0_3px_10px_rgb(0,0,0,0.2)] p-6`}>
            <div className="flex items-center gap-8 pb-4">
                <Filter />
                <p className="text-2xl font-semibold">Filters</p>
            </div>
            {groups.map((item, idx) => (
                <FilterGroup groupName={item.groupName} filterNames={item.filterNames} key={idx} />
            ))}
        </div>
    );
}