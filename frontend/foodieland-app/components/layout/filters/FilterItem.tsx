"use client";

import { CheckButton } from "@/components";
import { useState } from "react";

export default function FilterItem({ filterName, className }: { filterName: string, className?: string }) {
    const [isActive, setActive] = useState(false);

    function handleToggle() {
        setActive(!isActive);
    }

    return (
        <div className={`${className} flex items-center gap-4`}>
            <CheckButton isChecked={isActive} onToggle={handleToggle} />
            <p className="text-lg font-medium mt-2">{filterName}</p>
        </div>
    );
}