import { FilterItem } from "@/components";

export default function FilterGroup({ groupName, filterNames }: { groupName: string, filterNames: Array<string> }) {
    return (
        <div className="flex flex-col py-6 border-t-2 border-t-gray-200">
            <p className="text-xl font-semibold">{groupName}</p>
            {filterNames.map((filterName, idx) => (
                <FilterItem filterName={filterName} className="mt-4" key={idx} />
            ))}
        </div>
    );
}