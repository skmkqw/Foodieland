"use client";

import Link from "next/link";
import { ChevronLeft, ChevronRight } from "lucide-react";
import { useSearchParams } from "next/navigation";

export default function Pagination({ activePage, prevPage, nextPage, totalPages, className }: {
    activePage: number,
    prevPage: number,
    nextPage: number,
    totalPages: number,
    className?: string
}) {
    const pageNumbers: Array<number> = [];
    const searchParams = useSearchParams();

    for (let i = 1; i <= totalPages; i++) {
        pageNumbers.push(i);
    }

    const updatePageQuery = (pageNumber: number) => {
        const query = new URLSearchParams(searchParams.toString());
        query.set("page", pageNumber.toString());
        return `?${query.toString()}`;
    };

    return (
        <div className={`${className} flex items-center gap-6 text-xl font-medium`}>
            {activePage === 1 ? (
                <ChevronLeft />
            ) : (
                <Link href={updatePageQuery(prevPage)} aria-label="Previous page">
                    <ChevronLeft />
                </Link>
            )}

            {pageNumbers.map((pageNumber, idx) => (
                <Link
                    key={idx}
                    href={updatePageQuery(pageNumber)}
                    className={`${activePage === pageNumber ? "bg-primary" : "bg-gray-100"} rounded-full px-4 py-2 flex items-center justify-center`}
                >
                    {pageNumber}
                </Link>
            ))}

            {activePage === totalPages ? (
                <ChevronRight />
            ) : (
                <Link href={updatePageQuery(nextPage)} aria-label="Next page">
                    <ChevronRight />
                </Link>
            )}
        </div>
    );
}
