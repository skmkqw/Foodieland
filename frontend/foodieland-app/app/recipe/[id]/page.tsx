"use client";

import { useParams } from "next/navigation";

export default function RecipePage() {
    const { id } = useParams();
    return (
        <div>
            <h1>Recipe Detail Page</h1>
            <p>Recipe ID: {id}</p>
        </div>
    );
}