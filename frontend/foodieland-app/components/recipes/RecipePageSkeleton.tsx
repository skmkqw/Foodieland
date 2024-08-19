import { Container } from "@/components";

export default function RecipePageSkeleton() {
    return (
        <Container className="w-full flex flex-col py-5 sm:py-8 md:py-10">
            <div className="flex flex-col w-full gap-10 sm:gap-12 md:gap-16 animate-pulse">
                {/* Title Skeleton */}
                <div className="w-3/4 h-10 sm:h-12 md:h-14 bg-gray-300 rounded-lg mx-auto sm:mx-0"></div>

                {/* RecipeInfoBar Skeleton */}
                <div className="flex flex-col md:flex-row items-center gap-x-4 gap-y-10 md:gap-6">
                    <div className="flex items-center gap-6 w-full">
                        <div className="flex items-center gap-4 pr-4 md:px-6 border-r-2 border-r-gray-400">
                            <div className="w-12 h-12 bg-gray-300 rounded-full"></div>
                            <div className="w-24 h-6 bg-gray-300 rounded"></div>
                        </div>
                        <div className="flex gap-4 pr-4 md:pr-6 border-r-2 border-r-gray-400">
                            <div className="w-24 h-6 bg-gray-300 rounded"></div>
                        </div>
                        <div className="w-8 h-8 bg-gray-300 rounded-full"></div>
                    </div>
                </div>

                {/* Image and NutritionInformationCard Skeleton */}
                <div className="grid grid-cols-1 md:grid-cols-3 w-full gap-10">
                    <div className="md:col-span-2 h-96 bg-gray-300 rounded-3xl"></div>
                    <div className="h-96 bg-primary rounded-3xl"></div>
                </div>

                {/* Description Skeleton */}
                <div className="flex flex-col gap-3">
                    <div className="w-full h-3 bg-gray-300 rounded-lg"></div>
                    <div className="w-5/6 h-3 bg-gray-300 rounded-lg"></div>
                    <div className="w-3/4 h-3 bg-gray-300 rounded-lg"></div>
                </div>
            </div>
        </Container>
    );
}
